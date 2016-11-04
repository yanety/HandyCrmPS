﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace Handy.Crm.Powershell.Cmdlets
{
  [Cmdlet(VerbsCommon.New, "CRMEntity")]
  public class NewCrmEntityCommand : CrmExecuteMultipleCmdletBase
  {
    [Parameter(
      Mandatory = true)]
    [ValidateNotNullOrEmpty]
    public string EntityName { get; set; }

    [Parameter(
      Mandatory = true)]
    [ValidateNotNull]
    public Hashtable Attributes { get; set; }

    [Parameter(Mandatory = false)]
    public SwitchParameter SuppressDuplicateDetection { get; set; }

    protected override void ProcessRecord()
    {
      base.ProcessRecord();

      Entity entity = new Entity(EntityName);

      foreach (string key in Attributes.Keys)
      {
        entity[key] = Attributes[key].UnwrapPSObject();
      }

      CreateRequest createRequest = new CreateRequest
      {
        Target = entity
      };

      createRequest["SuppressDuplicateDetection"] = (bool)SuppressDuplicateDetection;

      WriteVerbose("Adding request to ExecuteMiltiple");
      _executeMultipleRequest.Requests.Add(createRequest);
    }

  }

}
