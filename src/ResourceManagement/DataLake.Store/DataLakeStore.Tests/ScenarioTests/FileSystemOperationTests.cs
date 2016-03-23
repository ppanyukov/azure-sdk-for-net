﻿//
// Copyright (c) Microsoft.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Rest.Azure;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.Azure.Management.DataLake.Store.Models;
using Xunit;
using Microsoft.Azure.Test.HttpRecorder;

namespace DataLakeStore.Tests
{
    public class FileSystemOperationTests : TestBase
    {
        #region internal constants

        internal const string folderToCreate = "SDKTestFolder01";
        internal const string folderToMove = "SDKTestMoveFolder01";
        internal const string fileToCreate = "SDKTestFile01.txt";
        internal const string fileToCreateWithContents = "SDKTestFile02.txt";
        internal const string fileToCopy = "SDKTestCopyFile01.txt";
        internal const string fileToConcatTo = "SDKTestConcatFile01.txt";
        internal const string fileToMove = "SDKTestMoveFile01.txt";

        internal const string fileContentsToAdd = "These are some random test contents 1234!@";
        internal const string fileContentsToAppend = "More test contents, that were appended!";

        #endregion
        private CommonTestFixture commonData;

        #region SDK Tests

        [Fact]
        public void DataLakeStoreFileSystemFolderCreate()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var folderPath = CreateFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, folderPath,
                        FileType.Directory, 0);
                }
            }
        }

        /*
        TODO: Re-enable code when Expiry is live on the server again
        [Fact]
        public void DataLakeStoreFileSystemSetAndRemoveExpiry()
        {
            const long maxTimeInMilliseconds = 253402300800000;
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, false, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File, 0);

                    // verify it does not have an expiration
                    var fileInfo = commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileInfo(filePath, commonData.DataLakeStoreFileSystemAccountName);
                    Assert.True(fileInfo.FileInfo.ExpirationTime <= 0 || fileInfo.FileInfo.ExpirationTime == maxTimeInMilliseconds, "Expiration time was not equal to 0 or DateTime.MaxValue.Ticks! Actual value reported: " + fileInfo.FileInfo.ExpirationTime);

                    // set the expiration time as an absolute value
                    
                    var toSetAbsolute = ToUnixTimeStampMs(HttpMockServer.GetVariable("absoluteTime", DateTime.Now.AddSeconds(120).ToString()));
                    commonData.DataLakeStoreFileSystemClient.FileSystem.SetFileExpiry(filePath, ExpiryOptionType.Absolute, commonData.DataLakeStoreFileSystemAccountName, toSetAbsolute);
                    fileInfo = commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileInfo(filePath, commonData.DataLakeStoreFileSystemAccountName);
                    VerifyTimeInAcceptableRange(toSetAbsolute, fileInfo.FileInfo.ExpirationTime.Value);

                    // set the expiration time relative to now
                    var toSetRelativeToNow = ToUnixTimeStampMs(HttpMockServer.GetVariable("relativeTime", DateTime.Now.AddSeconds(120).ToString()));
                    commonData.DataLakeStoreFileSystemClient.FileSystem.SetFileExpiry(filePath, ExpiryOptionType.RelativeToNow, commonData.DataLakeStoreFileSystemAccountName, 120 * 1000);
                    fileInfo = commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileInfo(filePath, commonData.DataLakeStoreFileSystemAccountName);
                    VerifyTimeInAcceptableRange(toSetRelativeToNow, fileInfo.FileInfo.ExpirationTime.Value);

                    // set expiration time relative to the creation time
                    var toSetRelativeCreationTime = fileInfo.FileInfo.CreationTime.Value + (120 * 1000);
                    commonData.DataLakeStoreFileSystemClient.FileSystem.SetFileExpiry(filePath, ExpiryOptionType.RelativeToCreationDate, commonData.DataLakeStoreFileSystemAccountName, 120 * 1000);
                    fileInfo = commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileInfo(filePath, commonData.DataLakeStoreFileSystemAccountName);
                    VerifyTimeInAcceptableRange(toSetRelativeCreationTime, fileInfo.FileInfo.ExpirationTime.Value);

                    // reset expiration time to never
                    commonData.DataLakeStoreFileSystemClient.FileSystem.SetFileExpiry(filePath, ExpiryOptionType.NeverExpire, commonData.DataLakeStoreFileSystemAccountName);
                    fileInfo = commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileInfo(filePath, commonData.DataLakeStoreFileSystemAccountName);
                    Assert.True(fileInfo.FileInfo.ExpirationTime <= 0 || fileInfo.FileInfo.ExpirationTime == maxTimeInMilliseconds, "Expiration time was not equal to 0 or DateTime.MaxValue.Ticks! Actual value reported: " + fileInfo.FileInfo.ExpirationTime);
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemNegativeExpiry()
        {
            const long maxTimeInMilliseconds = 253402300800000;
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, false, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File, 0);

                    // verify it does not have an expiration
                    var fileInfo = commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileInfo(filePath, commonData.DataLakeStoreFileSystemAccountName);
                    Assert.True(fileInfo.FileInfo.ExpirationTime <= 0 || fileInfo.FileInfo.ExpirationTime == maxTimeInMilliseconds, "Expiration time was not equal to 0 or DateTime.MaxValue.Ticks! Actual value reported: " + fileInfo.FileInfo.ExpirationTime);

                    // set the expiration time as an absolute value that is less than the creation time
                    var toSetAbsolute = ToUnixTimeStampMs(HttpMockServer.GetVariable("absoluteNegativeTime", DateTime.Now.AddSeconds(-120).ToString()));
                    Assert.Throws<CloudException>(() => commonData.DataLakeStoreFileSystemClient.FileSystem.SetFileExpiry(filePath, ExpiryOptionType.Absolute, commonData.DataLakeStoreFileSystemAccountName, toSetAbsolute));

                    // set the expiration time as an absolute value that is greater than max allowed time
                    toSetAbsolute = ToUnixTimeStampMs(DateTime.MaxValue.ToString()) + 1000;
                    Assert.Throws<CloudException>(() => commonData.DataLakeStoreFileSystemClient.FileSystem.SetFileExpiry(filePath, ExpiryOptionType.Absolute, commonData.DataLakeStoreFileSystemAccountName, toSetAbsolute));

                    // reset expiration time to never with a value and confirm the value is not honored
                    commonData.DataLakeStoreFileSystemClient.FileSystem.SetFileExpiry(filePath, ExpiryOptionType.NeverExpire, commonData.DataLakeStoreFileSystemAccountName, 400);
                    fileInfo = commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileInfo(filePath, commonData.DataLakeStoreFileSystemAccountName);
                    Assert.True(fileInfo.FileInfo.ExpirationTime <= 0 || fileInfo.FileInfo.ExpirationTime == maxTimeInMilliseconds, "Expiration time was not equal to 0 or DateTime.MaxValue.Ticks! Actual value reported: " + fileInfo.FileInfo.ExpirationTime);
                }
            }
        }
        */

        [Fact]
        public void DataLakeStoreFileSystemListFolderContents()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var folderPath = CreateFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, folderPath,
                        FileType.Directory, 0);

                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, false, true, folderPath);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File, 0);

                    // List all the contents in the folder
                    var listFolderResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.ListFileStatus(folderPath,
                        commonData.DataLakeStoreFileSystemAccountName);

                    // We know that this directory is brand new, so the contents should only be the one file.
                    Assert.Equal(1, listFolderResponse.FileStatuses.FileStatus.Count);
                    Assert.Equal(FileType.File, listFolderResponse.FileStatuses.FileStatus[0].Type);
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemEmptyFileCreate()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, false, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File, 0);
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemFileCreateWithContents()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File,
                        fileContentsToAdd.Length);
                    CompareFileContents(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath,
                        fileContentsToAdd);
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemAppendToFile()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, false, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File, 0);

                    // Append to the file that we created
                    commonData.DataLakeStoreFileSystemClient.FileSystem.Append(filePath,
                        new MemoryStream(Encoding.UTF8.GetBytes(fileContentsToAppend)),
                        commonData.DataLakeStoreFileSystemAccountName);

                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File,
                        fileContentsToAppend.Length);
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemConcatenateFiles()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath1 = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath1, FileType.File,
                        fileContentsToAdd.Length);

                    var filePath2 = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath2, FileType.File,
                        fileContentsToAdd.Length);

                    var targetFolder = CreateFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true);

                    commonData.DataLakeStoreFileSystemClient.FileSystem.Concat(
                        string.Format("{0}/{1}", targetFolder, fileToConcatTo),
                        new List<string> { filePath1, filePath2 },
                        commonData.DataLakeStoreFileSystemAccountName);

                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName,
                        string.Format("{0}/{1}", targetFolder, fileToConcatTo),
                        FileType.File,
                        fileContentsToAdd.Length*2);

                    // Attempt to get the files that were concatted together, which should fail and throw
                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath1,
                                commonData.DataLakeStoreFileSystemAccountName));
                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath2,
                                commonData.DataLakeStoreFileSystemAccountName));
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemMsConcatenateFiles()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath1 = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath1, FileType.File,
                        fileContentsToAdd.Length);

                    var filePath2 = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath2, FileType.File,
                        fileContentsToAdd.Length);

                    var targetFolder = CreateFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true);

                    commonData.DataLakeStoreFileSystemClient.FileSystem.MsConcat(
                        string.Format("{0}/{1}", targetFolder, fileToConcatTo),
                        new MemoryStream(Encoding.UTF8.GetBytes(string.Format("sources={0},{1}", filePath1, filePath2))),
                        commonData.DataLakeStoreFileSystemAccountName);

                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName,
                        string.Format("{0}/{1}", targetFolder, fileToConcatTo),
                        FileType.File,
                        fileContentsToAdd.Length*2);

                    // Attempt to get the files that were concatted together, which should fail and throw
                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath1,
                                commonData.DataLakeStoreFileSystemAccountName));
                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath2,
                                commonData.DataLakeStoreFileSystemAccountName));
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemMsConcatDeleteDir()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var concatFolderPath = string.Format("{0}/{1}", folderToCreate,
                        "msconcatFolder");
                    var filePath1 = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true,
                        concatFolderPath);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath1, FileType.File,
                        fileContentsToAdd.Length);

                    var filePath2 = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true,
                        concatFolderPath);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath2, FileType.File,
                        fileContentsToAdd.Length);

                    var targetFolder = CreateFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true);

                    commonData.DataLakeStoreFileSystemClient.FileSystem.MsConcat(
                        string.Format("{0}/{1}", targetFolder, fileToConcatTo),
                        new MemoryStream(Encoding.UTF8.GetBytes(string.Format("sources={0},{1}", filePath1, filePath2))),
                        commonData.DataLakeStoreFileSystemAccountName, deletesourcedirectory: true);

                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName,
                        string.Format("{0}/{1}", targetFolder, fileToConcatTo),
                        FileType.File,
                        fileContentsToAdd.Length*2);

                    // Attempt to get the files that were concatted together, which should fail and throw
                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath1,
                                commonData.DataLakeStoreFileSystemAccountName));
                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath2,
                                commonData.DataLakeStoreFileSystemAccountName));

                    // Attempt to get the folder that was created for concat, which should fail and be deleted.
                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(concatFolderPath,
                                commonData.DataLakeStoreFileSystemAccountName));
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemMoveFileAndFolder()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File,
                        fileContentsToAdd.Length);

                    var targetFolder1 = CreateFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true);
                    var targetFolder2 = TestUtilities.GenerateName(folderToMove);

                    // Move file first
                    var moveFileResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.Rename(filePath,
                        string.Format("{0}/{1}", targetFolder1, fileToMove), commonData.DataLakeStoreFileSystemAccountName);
                    Assert.True(moveFileResponse.OperationResult);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName,
                        string.Format("{0}/{1}", targetFolder1, fileToMove),
                        FileType.File,
                        fileContentsToAdd.Length);

                    // Ensure the old file is gone
                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath,
                                commonData.DataLakeStoreFileSystemAccountName));

                    // Now move folder completely.
                    var moveFolderResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.Rename(targetFolder1,
                        targetFolder2, commonData.DataLakeStoreFileSystemAccountName);
                    Assert.True(moveFolderResponse.OperationResult);

                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, targetFolder2,
                        FileType.Directory, 0);

                    // ensure all the contents of the folder moved
                    // List all the contents in the folder
                    var listFolderResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.ListFileStatus(targetFolder2,
                        commonData.DataLakeStoreFileSystemAccountName);

                    // We know that this directory is brand new, so the contents should only be the one file.
                    Assert.Equal(1, listFolderResponse.FileStatuses.FileStatus.Count);
                    Assert.Equal(FileType.File, listFolderResponse.FileStatuses.FileStatus[0].Type);

                    Assert.Throws(typeof (CloudException),
                        () =>
                            commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(targetFolder1,
                                commonData.DataLakeStoreFileSystemAccountName));
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemDeleteFolder()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var folderPath = CreateFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, folderPath,
                        FileType.Directory, 0);
                    DeleteFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, folderPath, true, false);
                    //WORK AROUND: Bug 4717659 makes it so even empty folders have contents.

                    // delete again expecting failure.
                    DeleteFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, folderPath, false, true);

                    // delete a folder with contents
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true, folderPath);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File,
                        fileContentsToAdd.Length);

                    // should fail if recurse is not set
                    DeleteFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, folderPath, false, true);

                    // Now actually delete
                    DeleteFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, folderPath, true, false);

                    // delete again expecting failure.
                    DeleteFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, folderPath, true, true);
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemDeleteFile()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true);
                    GetAndCompareFileOrFolder(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, FileType.File,
                        fileContentsToAdd.Length);
                    DeleteFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, false);

                    // try to delete it again, which should fail
                    DeleteFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, filePath, true);
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemGetAndSetAcl()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var currentAcl = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName);

                    var aclToReplaceWith = new List<string>(currentAcl.AclStatus.Entries);
                    var originalOther = string.Empty;
                    var toReplace = "other::rwx";
                    for (var i = 0; i < aclToReplaceWith.Count; i++)
                    {
                        if (aclToReplaceWith[i].StartsWith("other"))
                        {
                            originalOther = aclToReplaceWith[i];
                            aclToReplaceWith[i] = toReplace;
                            break;
                        }
                    }

                    Assert.False(string.IsNullOrEmpty(originalOther));

                    // Set the other acl to RWX
                    commonData.DataLakeStoreFileSystemClient.FileSystem.SetAcl("/",
                        string.Join(",", aclToReplaceWith),
                        commonData.DataLakeStoreFileSystemAccountName);

                    var newAcl = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName);
                    // verify the ACL actually changed

                    // Check the access first and assert that it returns OK (note: this is currently only for the user making the request, so it is not testing "other")
                    commonData.DataLakeStoreFileSystemClient.FileSystem.CheckAccess("/",
                        commonData.DataLakeStoreFileSystemAccountName, "rwx");

                    var foundIt = false;
                    foreach (var entry in newAcl.AclStatus.Entries.Where(entry => entry.StartsWith("other")))
                    {
                        foundIt = true;
                        Assert.Equal(toReplace, entry);
                        break;
                    }

                    Assert.True(foundIt);

                    // Set it back using specific entry
                    commonData.DataLakeStoreFileSystemClient.FileSystem.ModifyAclEntries("/",
                        originalOther, commonData.DataLakeStoreFileSystemAccountName);

                    // Now confirm that it equals the original ACL
                    var finalEntries = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName)
                        .AclStatus.Entries;
                    foreach (var entry in finalEntries)
                    {
                        Assert.True(
                            currentAcl.AclStatus.Entries.Any(
                                original => original.Equals(entry, StringComparison.CurrentCultureIgnoreCase)));
                    }

                    Assert.Equal(finalEntries.Count, currentAcl.AclStatus.Entries.Count);
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemSetFileProperties()
        {
            // This test simply tests that all bool/empty return actions return successfully
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (
                    commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var filePath = CreateFile(commonData.DataLakeStoreFileSystemClient, commonData.DataLakeStoreFileSystemAccountName, true, true);
                    var originalFileStatus =
                        commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath,
                            commonData.DataLakeStoreFileSystemAccountName)
                            .FileStatus;
                    // TODO: Set replication on file, this has been removed until it is confirmed as a supported API.
                    /*
                    var replicationResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.SetReplication(filePath,
                        commonData.DataLakeStoreFileSystemAccountName, 3);
                    Assert.True(replicationResponse.Boolean);
                    */

                    /*
                 * This API is available but all values put into it are ignored. Commenting this out until this API is fully functional.
                Assert.Equal(3,
                    dataLakeFileSystemClient.FileSystem.GetFileStatus(filePath, commonData.DataLakeStoreFileSystemAccountName)
                        .FileStatus.Replication);
                */

                    // set the time on the file
                    // We use a static date for now since we aren't interested in whether the value is set properly, only that the method returns a 200.
                    /* TODO: Re enable once supported.
                    var timeToSet = new DateTime(2015, 10, 26, 14, 30, 0).Ticks;
                    commonData.DataLakeStoreFileSystemClient.FileSystem.SetTimes(filePath,
                        commonData.DataLakeStoreFileSystemAccountName, timeToSet, timeToSet);

                    var fileStatusAfterTime =
                        commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(filePath,
                            commonData.DataLakeStoreFileSystemAccountName)
                            .FileStatus;
                    */

                    /*
                 * This API is available but all values put into it are ignored. Commenting this out until this API is fully functional.
                Assert.True(
                    fileStatusAfterTime.ModificationTime == timeToSet && fileStatusAfterTime.AccessTime == timeToSet);
                */

                    // TODO: Symlink creation is explicitly not supported, but when it is this should be enabled.
                    /*
                    var symLinkName = TestUtilities.GenerateName("testPath/symlinktest1");
                    Assert.Throws<CloudException>(() => commonData.DataLakeStoreFileSystemClient.FileSystem.CreateSymLink(filePath,
                        commonData.DataLakeStoreFileSystemAccountName, symLinkName, true));
                    */

                    // Once symlinks are available, remove the throws test and uncomment out this code.
                    // Assert.True(createSymLinkResponse.StatusCode == HttpStatusCode.OK);
                    // Assert.DoesNotThrow(() => dataLakeFileSystemClient.FileSystem.GetFileStatus(symLinkName, commonData.DataLakeStoreFileSystemAccountName));
                }

            }
        }

        [Fact]
        public void DataLakeStoreFileSystemGetAcl()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var aclGetResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName);

                    Assert.NotNull(aclGetResponse.AclStatus);
                    Assert.NotEmpty(aclGetResponse.AclStatus.Entries);
                    Assert.True(!string.IsNullOrEmpty(aclGetResponse.AclStatus.Owner));
                    Assert.True(!string.IsNullOrEmpty(aclGetResponse.AclStatus.Group));
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemSetAcl()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var aclGetResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName);

                    Assert.NotNull(aclGetResponse.AclStatus);
                    Assert.NotEmpty(aclGetResponse.AclStatus.Entries);

                    var currentCount = aclGetResponse.AclStatus.Entries.Count;
                    // add an entry to the ACL Entries
                    var newAcls = string.Join(",", aclGetResponse.AclStatus.Entries);
                    newAcls += string.Format(",user:{0}:rwx", commonData.AclUserId);

                    commonData.DataLakeStoreFileSystemClient.FileSystem.SetAcl("/",
                        newAcls, commonData.DataLakeStoreFileSystemAccountName);
                    
                    // retrieve the ACL again and confirm the new entry is present
                    aclGetResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName);

                    Assert.NotNull(aclGetResponse.AclStatus);
                    Assert.NotEmpty(aclGetResponse.AclStatus.Entries);
                    Assert.Equal(currentCount + 1, aclGetResponse.AclStatus.Entries.Count);
                    Assert.True(aclGetResponse.AclStatus.Entries.Any(entry => entry.Contains(commonData.AclUserId)));
                }
            }
        }

        [Fact]
        public void DataLakeStoreFileSystemSetDeleteAclEntry()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                commonData = new CommonTestFixture(context);
                using (commonData.DataLakeStoreFileSystemClient = commonData.GetDataLakeStoreFileSystemManagementClient(context))
                {
                    var aclGetResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName);

                    Assert.NotNull(aclGetResponse.AclStatus);
                    Assert.NotEmpty(aclGetResponse.AclStatus.Entries);

                    var currentCount = aclGetResponse.AclStatus.Entries.Count;
                    // add an entry to the ACL Entries
                    var newAce = string.Format("user:{0}:rwx", commonData.AclUserId);

                    commonData.DataLakeStoreFileSystemClient.FileSystem.ModifyAclEntries("/",
                         newAce, commonData.DataLakeStoreFileSystemAccountName);

                    // retrieve the ACL again and confirm the new entry is present
                    aclGetResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName);

                    Assert.NotNull(aclGetResponse.AclStatus);
                    Assert.NotEmpty(aclGetResponse.AclStatus.Entries);
                    Assert.Equal(currentCount + 1, aclGetResponse.AclStatus.Entries.Count);
                    Assert.True(aclGetResponse.AclStatus.Entries.Any(entry => entry.Contains(commonData.AclUserId)));

                    // now remove the entry
                    var aceToRemove = string.Format(",user:{0}", commonData.AclUserId);
                    commonData.DataLakeStoreFileSystemClient.FileSystem.RemoveAclEntries("/",
                        aceToRemove, commonData.DataLakeStoreFileSystemAccountName);

                    // retrieve the ACL again and confirm the new entry is present
                    aclGetResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.GetAclStatus("/",
                        commonData.DataLakeStoreFileSystemAccountName);

                    Assert.NotNull(aclGetResponse.AclStatus);
                    Assert.NotEmpty(aclGetResponse.AclStatus.Entries);
                    Assert.Equal(currentCount, aclGetResponse.AclStatus.Entries.Count);
                    Assert.False(aclGetResponse.AclStatus.Entries.Any(entry => entry.Contains(commonData.AclUserId)));
                }
            }
        }

        #endregion

        #region helpers
        internal string CreateFolder(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string caboAccountName, bool randomName = false)
        {
            // Create a folder
            var folderPath = randomName
                ? TestUtilities.GenerateName(folderToCreate)
                : folderToCreate;

            var response = commonData.DataLakeStoreFileSystemClient.FileSystem.Mkdirs(folderPath, caboAccountName);
            Assert.True(response.OperationResult);

            return folderPath;
        }

        internal string CreateFile(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string caboAccountName, bool withContents, bool randomName = false, string folderName = folderToCreate)
        {
            var filePath = randomName ? TestUtilities.GenerateName(string.Format("{0}/{1}", folderName, fileToCreate)) : string.Format("{0}/{1}", folderName, fileToCreate);

            if (!withContents)
            {
                commonData.DataLakeStoreFileSystemClient.FileSystem.Create(
                    filePath,
                    caboAccountName,
                    new MemoryStream());
            }
            else
            {
                commonData.DataLakeStoreFileSystemClient.FileSystem.Create(
                    filePath,
                    caboAccountName,
                    new MemoryStream(Encoding.UTF8.GetBytes(fileContentsToAdd)));
            }

            return filePath;
        }

        internal FileStatusResult GetAndCompareFileOrFolder(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string caboAccountName, string fileOrFolderPath, FileType expectedType, long expectedLength)
        {
            var getResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.GetFileStatus(fileOrFolderPath, caboAccountName);
            Assert.Equal(expectedLength, getResponse.FileStatus.Length);
            Assert.Equal(expectedType, getResponse.FileStatus.Type);

            return getResponse;
        }

        internal void CompareFileContents(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string caboAccountName, string filePath, string expectedContents)
        {
            // download a file and ensure they are equal
            Stream openResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.Open(filePath, caboAccountName, null);
            Assert.NotNull(openResponse);
            
            string toCompare = new StreamReader(openResponse).ReadToEnd();
            Assert.Equal(expectedContents, toCompare);
        }

        internal void DeleteFolder(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string caboAccountName, string folderPath, bool recursive, bool failureExpected)
        {
            if (failureExpected)
            {
                // try to delete a folder that doesn't exist or should fail
                try
                {
                    var deleteFolderResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.Delete(folderPath, caboAccountName, recursive);
                    Assert.True(!deleteFolderResponse.OperationResult);
                }
                catch (Exception e)
                {
                    Assert.Equal(typeof(CloudException), e.GetType());
                }
            }
            else
            {
                // Delete a folder
                var deleteFolderResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.Delete(folderPath, caboAccountName, recursive);
                Assert.True(deleteFolderResponse.OperationResult);
            }
        }

        internal void DeleteFile(DataLakeStoreFileSystemManagementClient dataLakeStoreFileSystemClient, string caboAccountName, string filePath, bool failureExpected)
        {
            if (failureExpected)
            {
                // try to delete a file that doesn't exist
                try
                {
                    var deleteFileResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.Delete(filePath, caboAccountName, false);
                    Assert.True(!deleteFileResponse.OperationResult);
                }
                catch (Exception e)
                {
                    Assert.Equal(typeof(CloudException), e.GetType());
                }
            }
            else
            {
                // Delete a file
                var deleteFileResponse = commonData.DataLakeStoreFileSystemClient.FileSystem.Delete(filePath, caboAccountName, false);
                Assert.True(deleteFileResponse.OperationResult);
            }
        }

        internal long ToUnixTimeStampMs(string date)
        {
            var convertedDate = Convert.ToDateTime(date);
            return (long)(convertedDate.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            
        }

        internal DateTime FromUnixTimestampMs(long unixTimestampInMs)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(unixTimestampInMs);
        }

        internal void VerifyTimeInAcceptableRange(long expected, long actual)
        {
            // We give a +- 100 ticks range due to timing constraints in the service.
            Assert.InRange<long>(actual, expected - 5000, expected + 5000);
        }

        #endregion
    }
}
