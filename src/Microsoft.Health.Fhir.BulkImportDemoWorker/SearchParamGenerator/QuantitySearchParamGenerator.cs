﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System.Data;
using Microsoft.Health.Fhir.Core.Features.Search.SearchValues;

namespace Microsoft.Health.Fhir.BulkImportDemoWorker.SearchParamGenerator
{
    public class QuantitySearchParamGenerator : ISearchParamGenerator
    {
        private ModelProvider _modelProvider;

        public QuantitySearchParamGenerator(ModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
        }

        public string TableName => "dbo.QuantitySearchParam";

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
            column.DataType = typeof(int);
            column.ColumnName = "SystemId";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "QuantityCodeId";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "SingleValue";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "LowValue";
            column.ReadOnly = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "HighValue";
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
            QuantitySearchValue searchValue = (QuantitySearchValue)searchParam.SearchIndexEntry.Value;
            bool isSingleValue = searchValue.Low == searchValue.High;

            DataRow row = table.NewRow();
            row["ResourceTypeId"] = _modelProvider.ResourceTypeMapping[searchParam.Resource.InstanceType];
            row["ResourceSurrogateId"] = searchParam.SurrogateId;
            row["SearchParamId"] = _modelProvider.SearchParamTypeMapping.ContainsKey(searchParam.SearchIndexEntry.SearchParameter.Url) ? _modelProvider.SearchParamTypeMapping[searchParam.SearchIndexEntry.SearchParameter.Url] : 0;
            row["SystemId"] = 0;
            row["QuantityCodeId"] = 0;
            row["SingleValue"] = isSingleValue ? searchValue.Low : null;
            row["LowValue"] = searchValue.Low ?? 0;
            row["HighValue"] = searchValue.High ?? 0;
            row["IsHistory"] = false;

            return row;
        }
    }
}
