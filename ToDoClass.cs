namespace TodoApp;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ToDoClass : INotifyPropertyChanged
{
    public ToDoClass()
    {
    }

    int _id;
    string _title = string.Empty;
    string _detail = string.Empty;

    public int id
    {
        get { return _id; }
        set { _id = value; OnPropertyChanged(nameof(id)); }
    }

    public string title
    {
        get { return _title; }
        set { _title = value ?? string.Empty; OnPropertyChanged(nameof(title)); }
    }

    public string detail
    {
        get { return _detail; }
        set { _detail = value ?? string.Empty; OnPropertyChanged(nameof(detail)); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
