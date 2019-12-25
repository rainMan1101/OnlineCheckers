using System;
using Domain.Entities;
using Domain.ValueTypes;

namespace Domain.Services.Repositories.Filters
{
    public interface ICheckerFilter
    {
        ICheckerFilter SetTeamType(EOperationType operationType, ETeamType teamType);

        ICheckerFilter SetIsAlive(EOperationType operationType, Boolean isAlive);

        ICheckerFilter SetIsKing(EOperationType operationType, Boolean isKing);

        ICheckerFilter BeetwenLocation(CLocation location1, CLocation location2);
    }
}
