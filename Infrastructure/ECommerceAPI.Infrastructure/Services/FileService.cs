﻿using ECommerceAPI.Application;
using ECommerceAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services;

public class FileService
{
    private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
    {
        string newFileName = await Task.Run(async () =>
        {
            string extensions = Path.GetExtension(fileName);
            string newFileName = string.Empty;

            if (!first)
            {
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extensions}";
            }
            else
            {
                newFileName = fileName;
                int indexNo1 = newFileName.IndexOf("-");
                if (indexNo1 == -1)
                {
                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extensions}";
                } else
                {
                    int lastIndex = 0;
                    while (true)
                    {
                        lastIndex = indexNo1;
                        indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                        if (indexNo1 == -1)
                        {
                            indexNo1 = lastIndex;
                            break;
                        }
                    }

                    int indexNo2 = newFileName.IndexOf(".");
                    string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                    if (int.TryParse(fileNo, out int _fileNo))
                    {
                        _fileNo++;
                        newFileName = newFileName.Remove(indexNo1, indexNo2 - indexNo1 - 1).Insert(indexNo1, _fileNo.ToString());
                    }
                    else
                    {
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extensions}";
                    }
                }
            }

            if (File.Exists($"{path}\\{newFileName}"))
            {
                return await FileRenameAsync(path, newFileName, false);
            }
            else return newFileName;
        });

        return newFileName;
    }

}
