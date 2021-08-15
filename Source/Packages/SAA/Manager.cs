namespace SAA
{
    public class Manager<TPoint> where TPoint : IPoint
    {
        public IPointGenerator<TPoint> Generator;
        public ITemperatureControl TemperatureControl;
        public Manager(IPointGenerator<TPoint>  generator,ITemperatureControl temperaturecontrol)
        {
            Generator = generator;
            TemperatureControl = temperaturecontrol;
        }
        public void GetResult(TPoint startpoint,Assess<TPoint> assess,out TPoint bestpoint,out double bestscore)
        {
            TemperatureControl.Inital();
            bestpoint = startpoint;
            bestscore = assess(startpoint);
            TPoint currentpoint = startpoint;
            double currentscore = bestscore;
            while(TemperatureControl.IsEnd())
            {
                TPoint newpoint = Generator.Next(currentpoint,TemperatureControl.T);
                double newscore = assess(newpoint);
                if(newscore<bestscore)
                {      
                    bestpoint = newpoint;
                    bestscore = newscore;
                }
                if(newscore<currentscore||TemperatureControl.Check(currentscore-newscore))
                {
                    currentpoint = newpoint;
                    currentscore = newscore;
                }
                TemperatureControl.Update();
            }
        }
    }
}
