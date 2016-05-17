# Orchard.Competition

## Installation

To install the full competition system you need to download the release of the different components and install it on [Orchard 1.6.1](https://orchard.codeplex.com/releases/view/90325).

1. Download [Orchard 1.6.1](https://orchard.codeplex.com/releases/view/90325).
2. Install Orchard on IIS or Azure WebApps. Choose the default recipe with the first run orchard.
3. Download the different packages
  - [Orchard.Competition](https://github.com/desjoerd/Orchard.Competition/releases), This contains the base components like scores, teams and objectives
  - [Orchard.Competition.ActionObjectives](https://github.com/desjoerd/Orchard.Competition.ActionObjectives/releases), This module contains specific objectives like a Photo, Video and question objectives.
  - [Orchard.Competition.Theme](https://github.com/desjoerd/Orchard.Competition.Theme/releases), This is the Theme build for the compition system and is based on Foundation.
  - [Orchard.Competition.Theme.Widgets](https://github.com/desjoerd/Orchard.Competition.Theme.Widgets/releases), Module which contains the Header for the page, because the Theme removes it.
4. Install the downloaded modules within the admin interface of Orchard.
5. Enable all the modules and set the theme to the foundation theme
6. Add the HeaderTitle Widget to the Header zone in the Admin interface.
7. Add a link to `/scoreboard` for the scoreboard.
8. Ready to go :)

## Compatibility
Compatible with [Orchard 1.6.1](https://orchard.codeplex.com/releases/view/90325).

## Development
1. Clone [Orchard](https://github.com/OrchardCMS/Orchard) and git checkout tag 1.6.1.
2. Copy the cloned module for example Orchard.Competition to `src/Orchard.Web/Modules` and the Theme to `src/Orchard.Web/Themes`.
3. Open the Orchard solution and add the projects.
4. Ready to debug and dev :).
