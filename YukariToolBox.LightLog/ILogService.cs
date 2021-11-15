using System.Globalization;

namespace YukariToolBox.LightLog;

/// <summary>
/// �Զ����log����
/// </summary>
public interface ILogService
{
    /// <summary>
    /// Info
    /// </summary>
    public void Info(string source, string message);

    /// <summary>
    /// Info
    /// </summary>
    public void Info<T>(string source, string message, T context);

    /// <summary>
    /// Warning
    /// </summary>
    public void Warning(string source, string message);

    /// <summary>
    /// Warning
    /// </summary>
    public void Warning<T>(string source, string message, T context);

    /// <summary>
    /// Error
    /// </summary>
    public void Error(string source, string message);

    /// <summary>
    /// Error
    /// </summary>
    public void Error(Exception exception, string source, string message);

    /// <summary>
    /// Error
    /// </summary>
    public void Error<T>(string source, string message, T context);

    /// <summary>
    /// Error
    /// </summary>
    public void Error<T>(Exception exception, string source, string message, T context);

    /// <summary>
    /// Fatal
    /// </summary>
    public void Fatal(Exception exception, string source, string message);

    /// <summary>
    /// Fatal
    /// </summary>
    public void Fatal<T>(Exception exception, string source, string message, T context);

    /// <summary>
    /// Debug
    /// </summary>
    public void Debug(string source, string message);

    /// <summary>
    /// Debug
    /// </summary>
    public void Debug<T>(string source, string message, T context);

    /// <summary>
    /// Verbos
    /// </summary>
    public void Verbos(string source, string message);

    /// <summary>
    /// Verbos
    /// </summary>
    public void Verbos<T>(string source, string message, T context);

    /// <summary>
    /// UnhandledExceptionLog
    /// </summary>
    public void UnhandledExceptionLog(UnhandledExceptionEventArgs args);

    /// <summary>
    /// ��������
    /// </summary>
    public void SetCultureInfo(CultureInfo cultureInfo);
}