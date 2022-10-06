    public class Signals
    {
        public class UnitScoreChanged
        {
            public int currentScore;

            public UnitScoreChanged(int score)
            {
                currentScore = score;
            }
        }
        
        public class LevelStarted
        {
            
        }

        public class LevelCompleted
        {
            
        }

        public class LevelFailed
        {
            
        }
        
        public class LevelReloadRequest
        {
            
        }
    }
