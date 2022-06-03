using BrewUp.Production.ReadModel.Abstracts;

namespace BrewUp.Production.ReadModel.Dtos;

public class LastEventPosition : DtoBase
{
    public long CommitPosition { get; set; }
    public long PreparePosition { get; set; }

    protected LastEventPosition()
    {}

    public LastEventPosition(string lastEventPositionKey, long commitPosition, long preparePosition)
    {
        Id = lastEventPositionKey;

        CommitPosition = commitPosition;
        PreparePosition = preparePosition;
    }
}