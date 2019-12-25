//using System;
//using System.Collections.Generic;
//using System.Data;
//using Domain.Enums;
//using Domain.ValueTypes;
//using Domain.Services.Repositories.Filters;

//namespace OnlineCheckers.DataStorage.Repositories.SQLFilters
//{
//    public class CCheckerFilter : CFilter, ICheckerFilter
//    {
//        public ICheckerFilter SetTeamType(EOperationType operationType, ETeamType teamType)
//        {
//            SetField("", teamType, SqlDbType.BigInt, operationType);

//            return this;
//        }

//        public ICheckerFilter SetIsAlive(EOperationType operationType, bool isAlive)
//        {
//            throw new NotImplementedException();
//        }

//        public ICheckerFilter SetIsKing(EOperationType operationType, bool isKing)
//        {
//            throw new NotImplementedException();
//        }

//        public ICheckerFilter BeetwenLocation(CLocation location1, CLocation location2)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
