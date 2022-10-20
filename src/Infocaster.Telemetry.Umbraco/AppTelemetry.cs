namespace Infocaster.Telemetry.Umbraco
{
    public class AppTelemetry<T> : IAppTelemetry
    {
        /// <summary>
        /// Name of the telemetry.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the telemetry.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Name of the telemetry value's original clr type.
        /// </summary>
        public string Type => typeof(T).ToString();

        public AppTelemetry(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}