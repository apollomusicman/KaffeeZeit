namespace KaffeeZeit.Server.Models
{

    public record Coworker
    {
        private Guid _id = Guid.NewGuid();
        private double _runningTab = 0;

        public required string Name { get; init; }

        public Guid Id { get { return _id; } }

        public double RunningTab
        {
            get { return _runningTab; }
        }

        public void AddToTab(double amount)
        {
            _runningTab += amount;
        }

        public void RemoveFromTab(double amount)
        {
            _runningTab -= amount;
        }
    }
}