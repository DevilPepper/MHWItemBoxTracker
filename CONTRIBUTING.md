# Contributing to MHWItemBoxTracker

<!-- Copied from here: https://github.com/semantic-release/semantic-release/blob/master/CONTRIBUTING.md -->

Thanks for contributing to **MHWItemBoxTracker**!

As a contributor, here are the guidelines we would like you to follow:
<!-- - [Code of conduct](#code-of-conduct) -->
- [How can I contribute?](#how-can-i-contribute)
- [Using the issue tracker](#using-the-issue-tracker)
- [Submitting a Pull Request](#submitting-a-pull-request)
- [Coding rules](#coding-rules)
  - Important! [Commit message format](#commit-message-format)

We also recommend that you read [How to Contribute to Open Source](https://opensource.guide/how-to-contribute).

## How can I contribute?

Start by forking the repo.

### Open and Close issues

Anything you work on, try to have an issue track it. Head over to the [issue tracker](https://github.com/SupaStuff/MHWItemBoxTracker/issues) and see if there's anything open that you would like to work on. If not [open an issue](#using-the-issue-tracker) and seek approval.

### Improve documentation

I'm not trying to spend too much time on documentation. If you see that documentation needs improvement, please feel free to improve it.

## Using the issue tracker

The issue tracker is the channel for [bug reports](#bug-report), [features requests](#feature-request) and [submitting pull requests](#submitting-a-pull-request) only.

Support, troubleshooting and questions... TBD.

Before opening an issue or a Pull Request, please use the [GitHub issue search](https://github.com/SupaStuff/MHWItemBoxTracker/issues) to make sure the bug or feature request hasn't been already reported or fixed. Probably comment on an issue to let everyone know you are working on it to keep people from steping on each other's toes. Would be better to assign the issue to yourself, though...

### Bug report

A good bug report shouldn't leave others needing to chase you for more information. Please try to be as detailed as possible in your report and fill the information requested in the [Bug report template](https://github.com/SupaStuff/MHWItemBoxTracker/issues/new?template=bug_report.md).

### Feature request

Feature requests are welcome, but take a moment to find out whether your idea fits with the scope and aims of the project. It's up to you to make a strong case to convince the project's developers of the merits of this feature. Please provide as much detail and context as possible and fill the information requested in the [Feature request template](https://github.com/SupaStuff/MHWItemBoxTracker/issues/new?template=feature_request.md).

## Submitting a Pull Request

Good pull requests, whether patches, improvements, or new features, are a fantastic help. They should remain focused in scope and avoid containing unrelated commits. When you create a pull request, you'll have to fill in the [template](https://github.com/SupaStuff/MHWItemBoxTracker/blob/master/.github/pull_request_template.md) to give us clear context on your work.

**Please have an open issue track your work** before embarking on a journey. I've been burned before because I didn't do this.

> Note: If you are stuck, feel free to open a pull request prefixed with [WIP] in the title in order to get feedback.

## Coding rules

### Source code

TBD

### Documentation

TBD

### Commit guidelines

#### Atomic commits

If possible, make [atomic commits](https://en.wikipedia.org/wiki/Atomic_commit), which means:

- a commit should contain exactly one self-contained functional change
- a functional change should be contained in exactly one commit
- a commit should not create an inconsistent state (such as test errors, linting errors, partial fix, feature with documentation etc...)

A complex feature can be broken down into multiple commits as long as each one maintains a consistent state and consists of a self-contained change.

#### Commit message format

Release versions and the changelog rely on [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) So you _**MUST**_ follow this convention. For a quick reference, this looks like:

```commit
<type>(<scope>): <description>
<BLANK LINE>
<optional body>
<BLANK LINE>
<optional footer(s)>
```

> Note: this can be acheived by using multiple `-m` options, but this might not be as nice as letting git open your text editor.

The **header** is mandatory and the **scope** of the header is optional and can be anything that decribes the scope of the change. Omit it if there are many scopes.

The **description** is self explanatory... Just follow these rules:

- use the imperative, present tense: "change" not "changed" nor "changes"
- don't capitalize first letter
- no dot (.) at the end

i.e.:

```bash
git commit -m "docs(contributing): add contibution guidelines"
```

Supported type are listed [here](https://github.com/conventional-changelog/conventional-changelog-config-spec/blob/master/versions/2.1.0/README.md#types), I believe.

The **body** can contain 1 or more [closing reference to an issue](https://help.github.com/articles/closing-issues-via-commit-messages).

The **footer** should be used to explain breaking changes. i.e.:

```commit
refactor!: drop support for json schema

BREAKING CHANGE: refactor to use embedded settings tool may lead to inconsistent behavior for users still using json schema to update settings.
```

Not the greatest example, but you get it.

#### Revert

If the commit reverts a previous commit, it should begin with `revert:`, followed by the header of the reverted commit. In the body it should say: `This reverts commit <hash>.`, where the hash is the SHA of the commit being reverted. i.e.:

```commit
revert: refactor!: drop support for json schema

This reverts commit 7af89ba
```
