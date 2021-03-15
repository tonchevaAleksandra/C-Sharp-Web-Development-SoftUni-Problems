namespace AspNetAppForTestingRazor.Services
{
    public class InstanceCounter : IInstanceCounter
    {
        private static int instances;

        public InstanceCounter()
        {
            instances++;
        }

        public int Instances => instances;
    }
}
