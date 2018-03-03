namespace KiteBrigade.Business
{
    public class Score
    {
        public static float Calculate(int windValue, int waterValue, int funValue)
        {
            return (windValue + waterValue + funValue) / 30f;
        }
    }
}
