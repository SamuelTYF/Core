namespace Argument
{
    public class Manager
    {
        public Dictionary<string, IConfig> Config;
        public Manager()
        {
            Config = new Dictionary<string, IConfig>();
        }
        public void Register(IConfig config)
        {
            foreach(string key in config.Params)
                Config[key] = config;
        }
        public bool Parse(string[] args)
        {
            int i = 0;
            while(i<args.Length)
            {
                string key= args[i++];
                if(Config.ContainsKey(key))
                {
                    IConfig config= Config[key];
                    if (config.Count + i > args.Length || !config.Parse(args, ref i))
                        return false;
                }
            }
            foreach (IConfig config in Config.Values)
                if (config.NeedValue && !config.Success)
                    return false;
            return true;
        }
        public string PrintHelper()
        {
            List<string> values = new();
            foreach (IConfig config in Config.Values)
                values.Add(config.Description);
            return string.Join("\n", values);
        }
    }
}
