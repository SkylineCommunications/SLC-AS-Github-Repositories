# GitHub Repositories

## About

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkylineCommunications_SLC-C-Github-Repositories&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=SkylineCommunications_SLC-C-Github-Repositories)

This **GitHub Repositories** solution is a visual layer on top of the *GitHub Repositories* connector that allows you to monitor and control GitHub repositories. It uses the GitHub API to poll the repositories and execute actions on them.

![General](./Github%20Repositories/CatalogInformation/Images/OrganizationsView.png)

## Getting Started

### Step 1: Deploy the GitHub Repositories package

1. Click the **Deploy** button to deploy the package to your DataMiner System.
1. Optionally, go to [admin.dataminer.services](https://admin.dataminer.services/), and verify whether the deployment was successful.

### Step 2: Generate a GitHub token

1. Go to [GitHub.com](https://github.com).
1. Sign in, and go to *Settings > Developer settings > Personal access tokens > Tokens (classic)*, and generate a new token.
1. Open the **GitHub Repositories** app, go to *Settings*, and enter the newly-generated GitHub token in the *API Key* box.

## Contributing to this solution

This solution is spread across the following GitHub repositories:

- The [Connector repository](https://github.com/SkylineCommunications/SLC-C-Github-Repositories), which handles all communication to and from GitHub.
- The [Connector API repository](https://github.com/SkylineCommunications/SLC-S-Github-Repositories), which handles communication to and from the **GitHub Repositories** element.
- The [Automation Script repository](https://github.com/SkylineCommunications/SLC-AS-Github-Repositories), which handles the script interactivity within the **GitHub Repositories** app.

To contribute to this solution, you can make a fork of the repositories, and create pull requests for the changes you want to make. We will then review the proposed changes as soon as possible and merge them.

## Use Cases

### Tracking issues across repositories

A simple use case could be creating an alarm template that will notify you when issues or pull requests come in for your repositories.

You could create one element per group of related repositories. For example, you could have a *Connector* repo, a *Connector API* repo, and an *Automation scripts* repo that all work together.

## Technical Reference

If you need any additional help, please reach out to [arne.maes@skyline.be](mailto:arne.maes@skyline.be).
