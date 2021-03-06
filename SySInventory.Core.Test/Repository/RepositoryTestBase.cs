﻿using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SySInventory.Core.Infrastructure.Persistence;
using SySInventory.Core.Repository;

namespace SySInventory.Core.Test.Repository
{
    [TestClass]
    public class RepositoryTestBase<T> : TestBase where T : IRepository
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
        public T Repository { get; set; }
        private IUnitOfWork Uow;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.InjectProperties(this);
            UnitOfWorkProvider = Container.Resolve<IUnitOfWorkProvider>();
            Repository = Container.Resolve<T>();
            Uow = UnitOfWorkProvider.BeginUnitOfWork();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            Uow.Rollback();
            base.TestCleanup();
        }

        protected void InUnitOfWork(Action action)
        {
            using (var uow = UnitOfWorkProvider.BeginUnitOfWork())
            {
                action();
                uow.Commit();
            }
        }

    }
}
