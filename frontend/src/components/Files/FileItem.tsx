import React, { useState } from 'react';
import type { FileInfo } from '../../types';
import DeleteConfirmation from './DeleteConfirmation';

interface FileItemProps {
  file: FileInfo;
  onDownload: (id: number, fileName: string) => void;
  onDelete: (id: number) => void;
}

const FileItem: React.FC<FileItemProps> = ({ file, onDownload, onDelete }) => {
  const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);

  const formatFileSize = (bytes: number): string => {
    if (bytes === 0) return '0 Bytes';
    const k = 1024;
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return Math.round((bytes / Math.pow(k, i)) * 100) / 100 + ' ' + sizes[i];
  };

  const formatDate = (dateString: string): string => {
    const date = new Date(dateString);
    return date.toLocaleString();
  };

  const handleDelete = () => {
    onDelete(file.id);
    setShowDeleteConfirm(false);
  };

  return (
    <>
      <tr className="hover:bg-gray-50">
        <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
          {file.originalFileName}
        </td>
        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
          {formatFileSize(file.sizeInBytes)}
        </td>
        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
          {formatDate(file.uploadedAt)}
        </td>
        <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium space-x-2">
          <button
            onClick={() => onDownload(file.id, file.originalFileName)}
            className="text-indigo-600 hover:text-indigo-900"
          >
            Download
          </button>
          <button
            onClick={() => setShowDeleteConfirm(true)}
            className="text-red-600 hover:text-red-900"
          >
            Delete
          </button>
        </td>
      </tr>

      {showDeleteConfirm && (
        <DeleteConfirmation
          fileName={file.originalFileName}
          onConfirm={handleDelete}
          onCancel={() => setShowDeleteConfirm(false)}
        />
      )}
    </>
  );
};

export default FileItem;
