namespace ClingoSharp
{
    public struct Tuple<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public Tuple(T1 first, T2 second)
        {
            Item1 = first;
            Item2 = second;
        }
    }

    public struct Tuple<T1, T2, T3>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }

        public Tuple(T1 first, T2 second, T3 third)
        {
            Item1 = first;
            Item2 = second;
            Item3 = third;
        }
    }
}