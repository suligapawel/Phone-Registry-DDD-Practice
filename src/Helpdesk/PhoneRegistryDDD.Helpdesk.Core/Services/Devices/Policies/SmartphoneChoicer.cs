﻿using System;
using PhoneRegistryDDD.Helpdesk.Core.Dictionaries;
using PhoneRegistryDDD.Helpdesk.Core.ValueObjects;

namespace PhoneRegistryDDD.Helpdesk.Core.Services.Devices.Policies;

public class SmartphoneChoicer : IDeviceChoicer
{
    private readonly EmployeeHrInfo _hrInfo;

    public SmartphoneChoicer(EmployeeHrInfo hrInfo)
    {
        _hrInfo = hrInfo;
    }

    public DeviceClass Choice()
    {
        if (WorkInManagement())
        {
            return DeviceClass.VIP;
        }

        if (IsResponsiblePerson())
        {
            return DeviceClass.Premium;
        }

        return DeviceClass.Standard;
    }

    private bool IsResponsiblePerson()
        => _hrInfo.Department.Contains("IT", StringComparison.OrdinalIgnoreCase)
           || _hrInfo.Position.Contains("Manager", StringComparison.OrdinalIgnoreCase);

    private bool WorkInManagement()
        => _hrInfo.Department.Contains("Management", StringComparison.OrdinalIgnoreCase)
           || _hrInfo.Position.Contains("Director", StringComparison.OrdinalIgnoreCase);
}