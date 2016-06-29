//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using DataServicesJSONP;

namespace WCFDataService
{
    [JSONPSupportBehavior]
    public class AntonyWcfDataService : DataService<AntonyEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            config.SetEntitySetAccessRule("Table_1", EntitySetRights.All);
            config.SetEntitySetAccessRule("Depts", EntitySetRights.All);
            config.SetEntitySetAccessRule("ParentTables", EntitySetRights.All);
            config.SetEntitySetAccessRule("ChildTables", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }
    }
}
