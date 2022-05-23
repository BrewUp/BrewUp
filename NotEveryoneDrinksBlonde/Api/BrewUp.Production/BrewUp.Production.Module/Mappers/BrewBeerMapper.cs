using System.Text;
using BrewUp.Production.Messages.Commands;
using Muflone.Messages;
using Muflone.Messages.Enums;
using Newtonsoft.Json;

namespace BrewUp.Production.Module.Mappers;

public class BrewBeerMapper : MessageMapper<BrewBeer>
{
    public override Message MapToMessage(BrewBeer request)
    {
        var message = new Message(new MessageHeader(request.MessageId, string.Empty, MessageType.MtNone),
            new MessageBody(JsonConvert.SerializeObject((object) request)));
        return base.MapToMessage(request);
    }

    public override BrewBeer MapToRequest(Message message)
    {
        var request = JsonConvert.DeserializeObject<BrewBeer>(Encoding.UTF8.GetString(message.Body.Bytes));

        return base.MapToRequest(message);
    }
}