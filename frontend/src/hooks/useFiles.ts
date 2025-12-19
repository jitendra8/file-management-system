import { useState, useEffect } from 'react';
import type { FileInfo } from '../types';
import { filesAPI } from '../services/api';

export const useFiles = () => {
  const [files, setFiles] = useState<FileInfo[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchFiles = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await filesAPI.getAll();
      setFiles(data);
    } catch (err: any) {
      setError(err.response?.data?.error || 'Failed to fetch files');
    } finally {
      setLoading(false);
    }
  };

  const uploadFile = async (file: File, onProgress?: (progress: number) => void) => {
    try {
      setError(null);
      await filesAPI.upload(file, onProgress);
      await fetchFiles();
    } catch (err: any) {
      const errorMsg = err.response?.data?.error || 'Failed to upload file';
      setError(errorMsg);
      throw new Error(errorMsg);
    }
  };

  const downloadFile = async (id: number, fileName: string) => {
    try {
      setError(null);
      await filesAPI.download(id, fileName);
    } catch (err: any) {
      setError(err.response?.data?.error || 'Failed to download file');
    }
  };

  const deleteFile = async (id: number) => {
    try {
      setError(null);
      await filesAPI.delete(id);
      await fetchFiles();
    } catch (err: any) {
      setError(err.response?.data?.error || 'Failed to delete file');
    }
  };

  useEffect(() => {
    fetchFiles();
  }, []);

  return {
    files,
    loading,
    error,
    uploadFile,
    downloadFile,
    deleteFile,
    refreshFiles: fetchFiles,
  };
};
