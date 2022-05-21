using System.Collections.Generic;
using System.Windows;

namespace FindInFilesUsingRegex;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MenuFileExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private async void BtnSearch_Click(object sender, RoutedEventArgs e)
    {
        string folder = txtStartFolder.Text;
        string regex = txtRegex.Text;

        Analyzer analyzer = new();
        List<File>? listFiles = await analyzer.FindInFolderAsync(folder, regex);

        listAll.ItemsSource = listFiles;
    }
}
