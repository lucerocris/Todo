using System.Collections.ObjectModel;

namespace TodoApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ToDoClass> TodoItems { get; } = new ObservableCollection<ToDoClass>();
    private int _nextId = 1;
    private ToDoClass? _selectedItem;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        todoLV.ItemsSource = TodoItems;
    }

    private void AddToDoItem(object sender, EventArgs e)
    {
        var title = titleEntry.Text?.Trim() ?? string.Empty;
        var detail = detailsEditor.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(title))
            return;

        TodoItems.Add(new ToDoClass
        {
            id = _nextId++,
            title = title,
            detail = detail
        });

        ClearForm();
    }

    private void TodoLV_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is not ToDoClass item)
            return;

        _selectedItem = item;
        titleEntry.Text = item.title;
        detailsEditor.Text = item.detail;
        addBtn.IsVisible = false;
        editBtn.IsVisible = true;
        cancelBtn.IsVisible = true;
    }

    private void todoLV_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        if (sender is ListView lv)
            lv.SelectedItem = null;
    }

    private void EditToDoItem(object sender, EventArgs e)
    {
        if (_selectedItem == null)
            return;

        var title = titleEntry.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(title))
            return;

        _selectedItem.title = title;
        _selectedItem.detail = detailsEditor.Text?.Trim() ?? string.Empty;

        ClearForm();
        _selectedItem = null;
        addBtn.IsVisible = true;
        editBtn.IsVisible = false;
        cancelBtn.IsVisible = false;
    }

    private void CancelEdit(object sender, EventArgs e)
    {
        ClearForm();
        _selectedItem = null;
        addBtn.IsVisible = true;
        editBtn.IsVisible = false;
        cancelBtn.IsVisible = false;
        todoLV.SelectedItem = null;
    }

    private void DeleteToDoItem(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not ToDoClass item)
            return;

        TodoItems.Remove(item);
        if (_selectedItem == item)
            CancelEdit(sender, e);
    }

    private void ClearForm()
    {
        titleEntry.Text = string.Empty;
        detailsEditor.Text = string.Empty;
        todoLV.SelectedItem = null;
    }
}
