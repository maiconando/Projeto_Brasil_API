using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Projeto.Models.Weather;
public record WeatherCityResponse
{
    [JsonIgnore]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }
    public string City { get; init; }
    public string State { get; init; }
    public DateTimeOffset UpdateOn { get; init; }
    public IEnumerable<WeatherCity> Weather { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
public record WeatherCity
{
    [JsonIgnore]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }
    public DateTime Date { get; init; }
    public string ConditionCode { get; init; }
    public string ConditionDescription { get; init; }
    public float? MinTemperature { get; init; }
    public float? MaxTemperature { get; init; }
    public float? UvIndex { get; init; }
}
public record WeatherAirportResponse
{
    [JsonIgnore]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }
    public int? Moisture { get; init; }
    public string Visibility { get; init; }
    public string ICAOCode { get; init; }
    public int? AtmosphericPressure { get; init; }
    public int? Wind { get; init; }
    public int? WindDirection { get; init; }
    public string ConditionCode { get; init; }
    public string ConditionDescription { get; init; }
    public int? Temperature { get; init; }
    public DateTimeOffset UpdateOn { get; init; }

}



