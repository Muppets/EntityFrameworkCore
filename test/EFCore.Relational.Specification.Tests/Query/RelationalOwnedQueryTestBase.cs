﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public abstract class RelationalOwnedQueryTestBase<TFixture> : OwnedQueryTestBase<TFixture>
        where TFixture : RelationalOwnedQueryTestBase<TFixture>.RelationalOwnedQueryFixture, new()
    {
        protected RelationalOwnedQueryTestBase(TFixture fixture)
            : base(fixture)
        {
        }

        public abstract class RelationalOwnedQueryFixture : OwnedQueryFixtureBase
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ListLoggerFactory;

            protected override QueryAsserter<PoolableDbContext> CreateQueryAsserter(
                Dictionary<Type, object> entitySorters,
                Dictionary<Type, object> entityAsserters)
                => new RelationalQueryAsserter<PoolableDbContext>(
                    CreateContext,
                    new OwnedQueryData(),
                    entitySorters,
                    entityAsserters,
                    CanExecuteQueryString);
        }
    }
}
