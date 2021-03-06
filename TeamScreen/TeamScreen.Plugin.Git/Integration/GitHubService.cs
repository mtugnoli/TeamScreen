﻿using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestEase;

namespace TeamScreen.Plugin.Git.Integration
{
    public interface IGitHubService
    {
        Task<GetCommitsResponse[]> GetCommits(string username, string password, string owner, string repo);
        Task<GetBranchesResponse[]> GetBranches(string username, string password, string owner, string repo);
        Task<GetTagsResponse[]> GetTags(string username, string password, string owner, string repo);
        Task<GetTagsResponse[]> GetCollaborators(string username, string password, string owner, string repo);
    }

    public class GitHubService : IGitHubService
    {
        private const string GitHubApiPath = "https://api.github.com/";

        public async Task<GetCommitsResponse[]> GetCommits(string username, string password, string owner, string repo)
        {
            var api = CreateApi(username, password);
            return await api.GetCommitsAsync(owner, repo);
        }

        public async Task<GetBranchesResponse[]> GetBranches(string username, string password, string owner, string repo)
        {
            var api = CreateApi(username, password);
            return await api.GetBranchesAsync(owner, repo);
        }

        public async Task<GetTagsResponse[]> GetTags(string username, string password, string owner, string repo)
        {
            var api = CreateApi(username, password);
            return await api.GetTagsAsync(owner, repo);
        }

        public async Task<GetTagsResponse[]> GetCollaborators(string username, string password, string owner, string repo)
        {
            var api = CreateApi(username, password);
            return await api.GetCollaboratorsAsync(owner, repo);
        }

        private IGitHubClient CreateApi(string username, string password)
        {
            var api = RestClient.For<IGitHubClient>(GitHubApiPath);
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            api.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            return api;
        }
    }
}