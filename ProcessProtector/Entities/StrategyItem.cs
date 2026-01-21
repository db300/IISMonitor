namespace ProcessProtector.Entities
{
    /// <summary>
    /// 策略实体
    /// </summary>
    internal class StrategyItem
    {
        /// <summary>
        /// 保护方式，0-立即重启，1-延时重启，2-执行脚本
        /// </summary>
        public int Method { get; set; } = 0;
        /// <summary>
        /// 延时秒数
        /// </summary>
        public int DelaySeconds { get; set; } = 60;
        /// <summary>
        /// 脚本文件名
        /// </summary>
        public string ScriptFileName { get; set; }
    }
}
