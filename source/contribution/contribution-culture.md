# Contribution Culture

## Culture and guides for our contributors

1. Don't raise a pull request (PR) that is not related to any issues or product backlog item (PBI).

1. Your pull request must have only changes (commits) that related to the related issue or PBI.

1. To make your pull request only have commits that are related to the related issue 

   - Create a new branch (with the name of the issue or PBI) from your main branch.
   - Switch to the new branch (git checkout new-branch-name).
   - Make the changes you want.
   - Add and commit your changes.
   - Push from your local to your new remote created branch.
   - Finally, open the pull request from your remote new branch to the original remote repo this will open a PR that has only one commit and only the related changes.
  
2. You can't assign an item to yourself directly, you should have the approval first.

3. If you want to work on any PBIs, you can ask for approval first, once you get the approved and assigned, you can start working.

4. You can't work on more than one PBIs at the same time (work sequential, not parallel) (WBS is one per contributor).

5. Once your item is in the review status you can pick another one.

6. Check the size of the PBI first, if it is large you can divide it into small items if it is possible.

7. Don't move item from new to Ready until it has size and priority.

8. Use comments in PBIs and Issues to discuss any topics and avoid using chat except urgent cases.

9. The commits, PR name should be well descriptive and not include the issue name only, use the following format for the PR name:
**Issue-number-Description**