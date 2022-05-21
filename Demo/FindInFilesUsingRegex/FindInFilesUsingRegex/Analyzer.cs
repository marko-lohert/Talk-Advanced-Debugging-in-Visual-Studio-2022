using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FindInFilesUsingRegex;

public class Analyzer
{
    public async Task<List<File>?> FindInFolderAsync(string folder, string regex)
    {
        if (string.IsNullOrWhiteSpace(folder) || regex is null)
            return await Task.FromResult<List<File>?>(null);

        List<File> listFiles = await FindDirectlyInFolder(folder, regex);

        listFiles.AddRange(await FindInSubfolders(folder, regex));

        return listFiles;
    }

    private async Task<List<File>> FindInSubfolders(string folder, string regex)
    {
        List<File> listFiles = new();

        List<string> allSubfoldersNames = GetAllSubfoldersNames(folder);

        foreach (string subfolder in allSubfoldersNames)
        {
            List<File>? filesInSubfolders = await FindInFolderAsync(subfolder, regex);

            if (filesInSubfolders is not null)
                listFiles.AddRange(filesInSubfolders);
        }

        return listFiles;
    }

    private async Task<List<File>> FindDirectlyInFolder(string folder, string regex)
    {
        List<string> allFileNames = GetAllFileNames(folder);

        List<File> listFiles = new();

        foreach (string fileName in allFileNames)
        {
            File? newAnalyzedFile = await FindInFileAsync(fileName, regex);

            if (newAnalyzedFile is not null)
                listFiles.Add(newAnalyzedFile);
        }

        return listFiles;
    }

    private List<string> GetAllFileNames(string folder)
    {
        if (string.IsNullOrWhiteSpace(folder))
            return new List<string>();

        DirectoryInfo dirInfo = new(folder);

        var allFiles = dirInfo.GetFiles(SearchPatternAllFiles, SearchOption.TopDirectoryOnly);

        List<string> allFullFileNames =
                           (from f in allFiles
                            select f.FullName)
                           .ToList();

        return allFullFileNames;
    }

    private List<string> GetAllSubfoldersNames(string folder)
    {
        if (folder is null)
            return new List<string>();

        DirectoryInfo currentFolder = new(folder);

        return currentFolder.GetDirectories().Select(x => x.FullName).ToList();

    }

    private async Task<File?> FindInFileAsync(string fullFileName, string regexPattern)
    {
        if (string.IsNullOrWhiteSpace(fullFileName) || regexPattern is null)
            return await Task.FromResult<File?>(null);

        File file = new()
        {
            Name = Path.GetFileName(fullFileName),
            Folder = Path.GetDirectoryName(fullFileName)
        };

        Regex regex = new(regexPattern);

        string fileContent = await System.IO.File.ReadAllTextAsync(fullFileName);
        MatchCollection matchCollection = regex.Matches(fileContent);

        file.CountMatches = matchCollection.Count;
        file.NumberOfLines = CalcNumberOfLines(fileContent.AsSpan());

        return file;
    }

    private int CalcNumberOfLines(ReadOnlySpan<char> fileContent)
    {
        if (fileContent == null)
            return 0;

        int numberOfLines = 0;

        int i = 0;
        do
        {
            if (fileContent[i] == '\n')
                numberOfLines++;

            i++;
        }
        while (i < fileContent.Length);

        return numberOfLines;
    }

    private const string SearchPatternAllFiles = "*.*";
}
