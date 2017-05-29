namespace Utils
{
    public class Singletone<TValue>
        where TValue : new()
    {
        public readonly static TValue Instance;

        static Singletone()
        {
            Instance = new TValue();
        }
    }
}
