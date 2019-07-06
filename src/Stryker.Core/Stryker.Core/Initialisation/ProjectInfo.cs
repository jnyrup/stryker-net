using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Stryker.Core.ProjectComponents;
using System.Collections.Generic;
using System.IO;

namespace Stryker.Core.Initialisation
{
    public class ProjectInfo
    {
        public ProjectAnalyzerResult TestProjectAnalyzerResult { get; set; }
        public ProjectAnalyzerResult ProjectUnderTestAnalyzerResult { get; set; }
        public bool FullFramework { get; set; }

        /// <summary>
        /// The Folder/File structure found in the project under test.
        /// </summary>
        public FolderComposite ProjectContents { get; set; }

        public string GetInjectionPath()
        {
            return FilePathUtils.ConvertPathSeparators(Path.Combine(
                FilePathUtils.ConvertPathSeparators(Path.GetDirectoryName(TestProjectAnalyzerResult.AssemblyPath)),
                FilePathUtils.ConvertPathSeparators(Path.GetFileName(ProjectUnderTestAnalyzerResult.AssemblyPath))));
        }

        public string GetTestBinariesPath()
        {
            return TestProjectAnalyzerResult.AssemblyPath;
        }
    }

    public class ProjectAnalyzerResult
    {
        private readonly ILogger _logger;
        private readonly object _analyzerResult;

        public ProjectAnalyzerResult(ILogger logger, object analyzerResult)
        {
            _logger = logger;
            _analyzerResult = analyzerResult;
        }

        private string _assemblyPath;
        public string AssemblyPath
        {
            get => null;
            set => _assemblyPath = value;
        }

        private IEnumerable<string> _projectReferences;
        public IEnumerable<string> ProjectReferences
        {
            get => null;
            set => _projectReferences = value;
        }

        private IReadOnlyDictionary<string, string> _properties;
        public IReadOnlyDictionary<string, string> Properties
        {
            get => null;
            set => _properties = value;
        }

        private string _targetFramework;
        public string TargetFramework
        {
            get => null;
            set => _targetFramework = value;
        }

        private IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> _packageReferences;
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> PackageReferences
        {
            get => null;
            set => _packageReferences = value;
        }

        private string _projectFilePath;
        public string ProjectFilePath
        {
            get => null;
            set => _projectFilePath = value;
        }

        private string[] _references;
        public string[] References
        {
            get => null;
            set => _references = value;
        }

        private bool? _signAssembly;
        public bool SignAssembly
        {
            get => false;
            set => _signAssembly = value;
        }

        private string _assemblyOriginatorKeyFile;
        public string AssemblyOriginatorKeyFile
        {
            get => null;
            set => _assemblyOriginatorKeyFile = value;
        }

        private IEnumerable<ResourceDescription> _resources;
        public IEnumerable<ResourceDescription> Resources
        {
            get
            {
                return _resources ?? EmbeddedResourcesGenerator.GetManifestResources(AssemblyPath, _logger);
            }
            set => _resources = value;
        }
    }
}
