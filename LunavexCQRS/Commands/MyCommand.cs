using System.IO;

namespace LunavexCQRS;

[Command(PackageIds.MyCommand)]
internal sealed class MyCommand : BaseCommand<MyCommand>
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        var docView = await VS.Documents.GetActiveDocumentViewAsync();
        string[] strings = FindPathAndName(docView);
        CreateCQRSFiles(strings, "Command");
        CreateCQRSFiles(strings, "CommandResponse");
        CreateCQRSFiles(strings, "CommandHandler");
        CreateCQRSFiles(strings, "Validator");
        UpdateCQRSFiles(strings, "Command");
        UpdateCQRSFiles(strings, "CommandResponse");
        UpdateCQRSFiles(strings, "CommandHandler");
        UpdateCQRSFiles(strings, "Validator");
        DeleteCQRSFiles(strings, "Command");
        DeleteCQRSFiles(strings, "CommandResponse");
        DeleteCQRSFiles(strings, "CommandHandler");
        DeleteCQRSFiles(strings, "Validator");
        GetAllCQRSFiles(strings, "Query");
        GetAllCQRSFiles(strings, "QueryResponse");
        GetAllCQRSFiles(strings, "QueryHandler");
        GetByIdCQRSFiles(strings, "Query");
        GetByIdCQRSFiles(strings, "QueryResponse");
        GetByIdCQRSFiles(strings, "QueryHandler");
        string results = "Dosyalar Başarıyla oluşturuldu...";
        await VS.MessageBox.ShowAsync(results, icon: Microsoft.VisualStudio.Shell.Interop.OLEMSGICON.OLEMSGICON_INFO, buttons: Microsoft.VisualStudio.Shell.Interop.OLEMSGBUTTON.OLEMSGBUTTON_OK);
    }
    private static string[] FindPathAndName(DocumentView docView)
    {
        string path = docView.FilePath;
        path = path.Replace(@"\", "/");
        string[] paths = path.Split('/');
        int count = paths.Length;
        string applicationName = "";

        for (int i = 0; i < count; i++)
        {
            if (paths[i].Contains("Domain"))
            {
                int index = paths[i].IndexOf("Domain");
                applicationName = paths[i].Substring(0, index);
                break;
            }
        }

        for (int i = 0; i < (count - 3); i++)
        {
            if (i > 0)
            {
                path = path + "/" + paths[i];
            }
            else
            {
                path = paths[i];
            }
        }

        string projectName = applicationName + "Application";
        string projePath = path + "/" + projectName + "/" + "Features";
        string selectedFileName = paths[count - 1];
        string[] strings = { projePath, selectedFileName, projectName };
        return strings;
    }
    private static void CreateCQRSFiles(string[] strings, string fileType)
    {
        string path = strings[0];
        string fileName = Path.GetFileNameWithoutExtension(strings[1]);
        string projectName = strings[2];

        string namespacePath = $"/{fileName}Features/Commands/Create{fileName}";
        path += $"{namespacePath}/Create{fileName}{fileType}";
        string namespaceString = namespacePath.Replace("/", ".");
        string fullPath = Path.Combine(path + ".cs");

        if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        }
        else if (File.Exists(fullPath))
        {
            Console.WriteLine("Dosya zaten mevcut.");
            return;
        }

        string[] contents;

        if (fileType == "Command")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed record Create{fileName}Command() : IRequest<Create{fileName}CommandResponse>;"
            };
        }
        else if (fileType == "CommandResponse")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed class Create{fileName}CommandResponse",
            "{",
            "}"
            };
        }
        else if (fileType == "CommandHandler")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public class Create{fileName}CommandHandler() : IRequestHandler<Create{fileName}Command,Create{fileName}CommandResponse>",
            "{",
            $"    public async Task<Create{fileName}CommandResponse> Handle(Create{fileName}Command request, CancellationToken cancellationToken)",
            "    {",
            "        throw new NotImplementedException();",
            "    }",
            "}"
            };
        }
        else if (fileType == "Validator")
        {
            contents = new string[]
            {
            "using FluentValidation;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed class Create{fileName}CommandValidator: AbstractValidator<Create{fileName}Command>",
            "{",
                $"    public Create{fileName}CommandValidator()",
                "    {",
                "    }",
            "}"
            };
        }
        else
        {
            throw new ArgumentException("Invalid file type.");
        }

        File.WriteAllLines(fullPath, contents);
    }
    private static void UpdateCQRSFiles(string[] strings, string fileType)
    {
        string path = strings[0];
        string fileName = Path.GetFileNameWithoutExtension(strings[1]);
        string projectName = strings[2];

        string namespacePath = $"/{fileName}Features/Commands/Update{fileName}";
        path += $"{namespacePath}/Update{fileName}{fileType}";
        string namespaceString = namespacePath.Replace("/", ".");
        string fullPath = Path.Combine(path + ".cs");

        if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        }
        else if (File.Exists(fullPath))
        {
            Console.WriteLine("Dosya zaten mevcut.");
            return;
        }

        string[] contents;

        if (fileType == "Command")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed record Update{fileName}Command() : IRequest<Update{fileName}CommandResponse>;"
            };
        }
        else if (fileType == "CommandResponse")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed class Update{fileName}CommandResponse",
            "{",
            "}"
            };
        }
        else if (fileType == "CommandHandler")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public class Update{fileName}CommandHandler() : IRequestHandler<Update{fileName}Command,Update{fileName}CommandResponse>",
            "{",
            $"    public async Task<Update{fileName}CommandResponse> Handle(Update{fileName}Command request, CancellationToken cancellationToken)",
            "    {",
            "        throw new NotImplementedException();",
            "    }",
            "}"
            };
        }
        else if (fileType == "Validator")
        {
            contents = new string[]
            {
            "using FluentValidation;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed class Update{fileName}CommandValidator: AbstractValidator<Update{fileName}Command>",
            "{",
                $"    public Update{fileName}CommandValidator()",
                "    {",
                "    }",
            "}"
            };
        }
        else
        {
            throw new ArgumentException("Invalid file type.");
        }

        File.WriteAllLines(fullPath, contents);
    }
    private static void DeleteCQRSFiles(string[] strings, string fileType)
    {
        string path = strings[0];
        string fileName = Path.GetFileNameWithoutExtension(strings[1]);
        string projectName = strings[2];

        string namespacePath = $"/{fileName}Features/Commands/Delete{fileName}";
        path += $"{namespacePath}/Delete{fileName}{fileType}";
        string namespaceString = namespacePath.Replace("/", ".");
        string fullPath = Path.Combine(path + ".cs");

        if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        }
        else if (File.Exists(fullPath))
        {
            Console.WriteLine("Dosya zaten mevcut.");
            return;
        }

        string[] contents;

        if (fileType == "Command")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed record Delete{fileName}Command() : IRequest<Delete{fileName}CommandResponse>;"
            };
        }
        else if (fileType == "CommandResponse")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed class Delete{fileName}CommandResponse",
            "{",
            "}"
            };
        }
        else if (fileType == "CommandHandler")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public class Delete{fileName}CommandHandler() : IRequestHandler<Delete{fileName}Command,Delete{fileName}CommandResponse>",
            "{",
            $"    public async Task<Delete{fileName}CommandResponse> Handle(Delete{fileName}Command request, CancellationToken cancellationToken)",
            "    {",
            "        throw new NotImplementedException();",
            "    }",
            "}"
            };
        }
        else if (fileType == "Validator")
        {
            contents = new string[]
            {
            "using FluentValidation;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed class Delete{fileName}CommandValidator: AbstractValidator<Delete{fileName}Command>",
            "{",
                $"    public Delete{fileName}CommandValidator()",
                "    {",
                "    }",
            "}"
            };
        }
        else
        {
            throw new ArgumentException("Invalid file type.");
        }

        File.WriteAllLines(fullPath, contents);
    }
    private static void GetAllCQRSFiles(string[] strings, string fileType)
    {
        string path = strings[0];
        string fileName = Path.GetFileNameWithoutExtension(strings[1]);
        string projectName = strings[2];

        string namespacePath = $"/{fileName}Features/Queries/GetAll{fileName}";
        path += $"{namespacePath}/GetAll{fileName}{fileType}";
        string namespaceString = namespacePath.Replace("/", ".");
        string fullPath = Path.Combine(path + ".cs");

        if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        }
        else if (File.Exists(fullPath))
        {
            Console.WriteLine("Dosya zaten mevcut.");
            return;
        }

        string[] contents;

        if (fileType == "Query")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed record GetAll{fileName}Query() : IRequest<GetAll{fileName}QueryResponse>;"
            };
        }
        else if (fileType == "QueryResponse")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed class GetAll{fileName}QueryResponse",
            "{",
            "}"
            };
        }
        else if (fileType == "QueryHandler")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public class GetAll{fileName}QueryHandler() : IRequestHandler<GetAll{fileName}Query,GetAll{fileName}QueryResponse>",
            "{",
            $"    public async Task<GetAll{fileName}QueryResponse> Handle(GetAll{fileName}Query request, CancellationToken cancellationToken)",
            "    {",
            "        throw new NotImplementedException();",
            "    }",
            "}"
            };
        }
        else
        {
            throw new ArgumentException("Invalid file type.");
        }

        File.WriteAllLines(fullPath, contents);
    }
    private static void GetByIdCQRSFiles(string[] strings, string fileType)
    {
        string path = strings[0];
        string fileName = Path.GetFileNameWithoutExtension(strings[1]);
        string projectName = strings[2];

        string namespacePath = $"/{fileName}Features/Queries/Get{fileName}ById";
        path += $"{namespacePath}/GetAll{fileName}{fileType}";
        string namespaceString = namespacePath.Replace("/", ".");
        string fullPath = Path.Combine(path + ".cs");

        if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        }
        else if (File.Exists(fullPath))
        {
            Console.WriteLine("Dosya zaten mevcut.");
            return;
        }

        string[] contents;

        if (fileType == "Query")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed record Get{fileName}ByIdQuery() : IRequest<Get{fileName}ByIdQueryResponse>;"
            };
        }
        else if (fileType == "QueryResponse")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public sealed class Get{fileName}ByIdQueryResponse",
            "{",
            "}"
            };
        }
        else if (fileType == "QueryHandler")
        {
            contents = new string[]
            {
            "using MediatR;",
            "",
            $"namespace {projectName}.Features{namespaceString};",
            $"public class Get{fileName}ByIdQueryHandler() : IRequestHandler<Get{fileName}ByIdQuery,Get{fileName}ByIdQueryResponse>",
            "{",
            $"    public async Task<Get{fileName}ByIdQueryResponse> Handle(Get{fileName}ByIdQuery request, CancellationToken cancellationToken)",
            "    {",
            "        throw new NotImplementedException();",
            "    }",
            "}"
            };
        }
        else
        {
            throw new ArgumentException("Invalid file type.");
        }

        File.WriteAllLines(fullPath, contents);
    }
}
