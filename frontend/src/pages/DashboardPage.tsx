import Layout from '../components/Layout/Layout';
import FileUpload from '../components/Files/FileUpload';
import FileList from '../components/Files/FileList';
import { useFiles } from '../hooks/useFiles';

const DashboardPage = () => {
  const { files, loading, error, uploadFile, downloadFile, deleteFile } = useFiles();

  return (
    <Layout>
      <div className="space-y-8">
        <div>
          <h2 className="text-2xl font-bold text-gray-900 mb-4">Upload File</h2>
          <FileUpload onUpload={uploadFile} />
        </div>

        {error && (
          <div className="rounded-md bg-red-50 p-4">
            <p className="text-sm text-red-800">{error}</p>
          </div>
        )}

        <div>
          <h2 className="text-2xl font-bold text-gray-900 mb-4">Your Files</h2>
          <div className="bg-white shadow rounded-lg overflow-hidden">
            <FileList
              files={files}
              loading={loading}
              onDownload={downloadFile}
              onDelete={deleteFile}
            />
          </div>
        </div>
      </div>
    </Layout>
  );
};

export default DashboardPage;
