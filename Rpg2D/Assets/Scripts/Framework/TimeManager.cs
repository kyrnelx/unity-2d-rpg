using UnityEngine;

public class TimeManager
{
    private static TimeManager m_instance;
    public static TimeManager Instance => m_instance ?? (m_instance = new TimeManager());

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }
}