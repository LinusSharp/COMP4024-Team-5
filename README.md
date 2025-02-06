# 🖥️ Welcome to the Team 5 Git repo ! 

## 👾 Project 

### 📑 Requirements:
* The game must be developed in Unity
* 2D game
* Target audience : Kids aged between 10-14 years.

### 📊 Developement process and team work : 

* Project planning: 
    * Use Trello and GitHub to manage tasks.
    * Define use cases to describe expected player interactions 

* Git workflow: 
    * Use branches to collaborate
    * Follow good commit practices 

* Testing : 
    * Implement unit tests. 


### 📅 Milestones: 
* Create a low-fidelity prototype
* Create a basic 2D game with the essential features
* Stretch goal : add levels of difficulty to the game and more features. 

## Branching convention: 
* Try not to add more than one feature before comitting.
* Try not to fix more than one issue before comitting.
* Commit OFTEN to your local branch, but not to main.
* Keep commit messages short and to the point. If its too long, you probably fixed too much before commiting!

## Merging conventions: 
* To merge a branch: git merge <target-branch>
* When creating a merge request:
    * Select target on GitLab
    * Select appropriate Asignee
    * Merge Options: Select BOTH Delete source branch AND Squash Commits.
* Create Merge Request.

## Commit conventions: 
* feat Commits, that adds or remove a new feature
* fix Commits, that fixes a bug
* refactor Commits, that rewrite/restructure your code
* style Commits, that do not affect the meaning (white-space, formatting, missing semi-colons, etc)
* test Commits, that add missing tests or correcting existing tests
* docs Commits, that affect documentation only
* build Commits, that affect build components like build tool, ci pipeline, dependencies, project version, 

## Git Cheatsheet: 
* Create and switch to branch: git checkout -b <branch-name> -> remove Flag: -b to switch to existing branch
* Connect your branch to the correct target: git push --set-upstream origin <branch-name>
* See all branches git branch
* See previous commits, and their ID's git log --oneline