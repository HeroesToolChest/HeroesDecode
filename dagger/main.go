package main

import (
	"context"
	"dagger/heroes-decode/internal/dagger"
)

type HeroesDecode struct{}

/*

docker run heroes-decode:latest
Option '--replay-path' is required.

Description:
  View Heroes of the Storm replay file data

Usage:
  HeroesDecode [command] [options]

Options:
  -p, --replay-path <replay-path> (REQUIRED)  File path of a Heroes of the Storm .StormReplay file or a directory
  --result-only                               Will only show result of parsing, no map info or player info; --show-player-talents and --show-player-stats options will be overridden to false [default: False]
  -t, --show-player-talents                   Shows the player's talent information [default: False]
  -s, --show-player-stats                     Shows the player's stats [default: False]
  --version                                   Show version information
  -?, -h, --help                              Show help and usage information

Commands:
  pregame           View Heroes of the Storm battlelobby file data.
  get-json          Get the data from the replay as json
  get-pregame-json  Get the data from the replay battlelobby as json

*/

func (m *HeroesDecode) Build(ctx context.Context, dir *dagger.Directory) *dagger.Container {

	// Alternatively use the built in .NET 8 SDK tarball export, but requires .NET csproj modification
	build := dag.
		Container().
		From("mcr.microsoft.com/dotnet/sdk:8.0").
		WithWorkdir("/app").
		WithDirectory("/app", dir).
		WithExec([]string{"dotnet", "publish", "-c", "Release"})

	return dag.
		Container().
		From("mcr.microsoft.com/dotnet/runtime:8.0").
		WithWorkdir("/app").
		WithDirectory("/app", build.Directory("/app/bin/Release/net8.0/publish")).
		WithEntrypoint([]string{"./HeroesDecode"})
}
