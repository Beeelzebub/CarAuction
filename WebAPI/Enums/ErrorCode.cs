﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums
{
    public enum ErrorCode
    {
        Success,
        LotNotFoundError,
        CarNotFound,
        AlreadyPlacedBetError,
        WrongUsernameOrPasswordError,
        RegistrationError,
        NoPermissionsError,
        InternalServerError
    }
}