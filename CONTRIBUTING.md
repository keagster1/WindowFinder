## Contributing

First off, thank you for considering contributing to WindowFinder!

⚠️ This is mostly a copy & paste from https://github.com/activeadmin/activeadmin/blob/HEAD/CONTRIBUTING.md Please let me know something doesn't make sense!⚠️

### Where do I go from here?

If you've noticed a bug or have a feature request, [make one][new issue]! It's
generally best if you get confirmation of your bug or approval for your feature
request this way before starting to code.

If you have a general question about WindowFinder, you can post it on [Stack
Overflow], the issue tracker is only for bugs and feature requests.

### Fork & create a branch

If this is something you think you can fix, then [fork WindowFinder] and create
a branch with a descriptive name.

A good branch name would be (where issue #325 is the ticket you're working on):

```sh
git checkout -b 325-add-japanese-translations
```
### Implement your fix or feature

At this point, you're ready to make your changes! Feel free to ask for help;
everyone is a beginner at first :smile_cat:

### Get the style right

Your patch should follow the same conventions & be at the same standard/quality as the rest. UI changes should stay as minimal as possible.

### Make a Pull Request

At this point, you should switch back to your master branch and make sure it's
up to date with WindowFinder's master branch:

```sh
git remote add upstream git@github.com:WindowFinder/WindowFinder.git
git checkout master
git pull upstream master
```

Then update your feature branch from your local copy of master, and push it!

```sh
git checkout 325-add-japanese-translations
git rebase master
git push --set-upstream origin 325-add-japanese-translations
```

Finally, go to GitHub and [make a Pull Request][] :D

Github Actions will run our test suite against all supported Rails versions. We
care about quality, so your PR won't be merged until all tests pass. It's
unlikely, but it's possible that your changes pass tests in one Rails version
but fail in another. In that case, you'll have to setup your development
environment (as explained in step 3) to use the problematic Rails version, and
investigate what's going on!

### Keeping your Pull Request updated

If a maintainer asks you to "rebase" your PR, they're saying that a lot of code
has changed, and that you need to update your branch so it's easier to merge.

To learn more about rebasing in Git, there are a lot of [good][git rebasing]
[resources][interactive rebase] but here's the suggested workflow:

```sh
git checkout 325-add-japanese-translations
git pull --rebase upstream master
git push --force-with-lease 325-add-japanese-translations
```

### Merging a PR (maintainers only)

A PR can only be merged into master by a maintainer if:

* Currently I am requiring personal approval from repo owner.

### Shipping a release (maintainers only)

Maintainers need to do the following to push out a release:

* Switch to the master branch and make sure it's up to date.
* `Build the WindowFinder main solution`
* `Build the WindowFinder installer solution`
* Rename Setup1.msi and Setup1.exe to WindowFinderSetup.msi/exe
* Review and submit the PR. The generated changelog in the PR should include all user visible changes you intend to ship.

[Stack Overflow]: http://stackoverflow.com/questions/tagged/WindowFinder
[new issue]: https://github.com/activeadmin/WindowFinder/issues/new
[fork WindowFinder]: https://help.github.com/articles/fork-a-repo
[make a pull request]: https://help.github.com/articles/creating-a-pull-request
[git rebasing]: http://git-scm.com/book/en/Git-Branching-Rebasing