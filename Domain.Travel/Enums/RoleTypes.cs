
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Enums
{
    public enum RoleTypes
    {
        User=1,
        Owner=2,
        Admin=3
    }
    //public static class RoleTypesExtensions
    //{
    //    public static ObjectId ToObjectId(this RoleTypes roleType)
    //    {
    //        return ObjectId.Parse(((int)roleType).ToString("X").PadLeft(24, '0'));
    //    }

    //    public static RoleTypes ToRoleType(this ObjectId objectId)
    //    {
    //        int intValue = Convert.ToInt32(objectId.ToString(), 16);
    //        return (RoleTypes)Enum.ToObject(typeof(RoleTypes), intValue);
    //    }
    //}
}
