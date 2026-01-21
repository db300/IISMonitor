namespace ProcessProtector.Entities
{
    /// <summary>
    /// 进程守护实体
    /// </summary>
    internal class ProcessProtectItem
    {
        /// <summary>
        /// 进程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 进程路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 保护状态
        /// </summary>
        public string Status { get; set; } = "未知";
    }
}
