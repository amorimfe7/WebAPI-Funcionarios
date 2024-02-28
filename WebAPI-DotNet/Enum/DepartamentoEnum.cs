using System.Text.Json.Serialization;

namespace WebAPI_DotNet.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DepartamentoEnum
    {
        Atendimento,
        Compras,
        Desenvolvimento,
        Financeiro,
        RH,
        Zeladoria
    }
}
