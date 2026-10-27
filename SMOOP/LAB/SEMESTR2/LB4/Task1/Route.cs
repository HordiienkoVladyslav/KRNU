using System;


public class Route
{
    public string StartPoint { get; set; } = string.Empty;
    public string EndPoint { get; set; } = string.Empty;
    public double TravelTime { get; set; } // Час подорожі у годинах
    public int RouteNumber { get; set; }

    public Route() { }

    public Route(string startPoint, string endPoint, double travelTime, int routeNumber)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        TravelTime = travelTime;
        RouteNumber = routeNumber;
    }

    public override string ToString() =>
        $"Маршрут №{RouteNumber}: {StartPoint} -> {EndPoint}, Час: {TravelTime} год.";
}