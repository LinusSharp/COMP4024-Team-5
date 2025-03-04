# 🖥️ Welcome to the Team 5 Git repo ! 

## 👾 Project 

### Game Workflow:
**Tutorial Level**  -  A short introductory level teaching players the basic controls and mechanics 

**Lobby Area** - The hub where the motherboard is displayed; collected parts are placed here.

**Level Selector** - Players progressively unlock and select levels, each representing a computer part.

**Platformer Levels** -  Players navigate obstacles  to collect a computer component at the end.

**Completion** - Once all four parts are collected, the computer is fully assembled


### 📊 Developement process and team work : 

* 🗂️ Project planning: 
    * 📌 Use Trello and GitHub to manage tasks.
    * 📌 Define use cases to describe expected player interactions 

* 🛠️ Git workflow:  
    * 📌 Use branches to collaborate
    * 📌 Follow good commit practices 

* 🧪 Testing standards: 
    * 📌  Use the Unity Testing Framework
    * 📌 If tests involve MonoBehaviour, they should be written in PlayMode Tests
    * 📌  Naming Convention for Tests: Something_DoesSomething_ThisHappens


### 📅 Milestones: 
* Create a low-fidelity prototype
* Create a basic 2D game with the essential features
* Stretch goal : add levels of difficulty to the game and more features. 

## 🌱 Branching convention: 
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