﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Data;
using Microsoft.Health.Fhir.Core.Features.Search.SearchValues;

namespace Microsoft.Health.Fhir.BulkImportDemoWorker.SearchParamGenerator
{
    public class ReferenceTokenCompositeSearchParamGenerator : ISearchParamGenerator
    {
        private ModelProvider _modelProvider;

        public ReferenceTokenCompositeSearchParamGenerator(ModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
        }

        public string TableName => "dbo.ReferenceTokenCompositeSearchParam";

        public DataTable CreateDataTable()
        {
            DataTable table = new DataTable("DataTable");
            DataColumn column;

            column = new DataColumn();
            column.DataType = typeof(short);
            column.ColumnName = "ResourceTypeId";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(long);
            column.ColumnName = "ResourceSurrogateId";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(short);
            column.ColumnName = "SearchParamId";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "BaseUri1";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(short);
            column.ColumnName = "ReferenceResourceTypeId1";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ReferenceResourceId1";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "ReferenceResourceVersion1";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "SystemId2";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Code2";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "IsHistory";
            column.ReadOnly = true;
            table.Columns.Add(column);

            return table;
        }

        public DataRow GenerateDataRow(DataTable table, BulkCopySearchParamWrapper searchParam)
        {
            var content = ((CompositeSearchValue)searchParam.SearchIndexEntry.Value).Components;
            var reference = (ReferenceSearchValue)content[0][0];
            var token = (TokenSearchValue)content[1][0];

            DataRow row = table.NewRow();
            row["ResourceTypeId"] = _modelProvider.ResourceTypeMapping[searchParam.Resource.InstanceType];
            row["ResourceSurrogateId"] = searchParam.SurrogateId;
            row["SearchParamId"] = _modelProvider.SearchParamTypeMapping.ContainsKey(searchParam.SearchIndexEntry.SearchParameter.Url) ? _modelProvider.SearchParamTypeMapping[searchParam.SearchIndexEntry.SearchParameter.Url] : 0;
            row["IsHistory"] = false;
            ReferenceSearchParamGenerator.FillInRow(row, reference, _modelProvider, "1");
            TokenSearchParamGenerator.FillInRow(row, token, "2");

            return row;
        }
    }
}