# Run tests and generate code coverage report

1. Run `go test -v -coverprofile=cover.text`
2. Run `go tool cover -html=cover.text -o cover.html`
3. Open `cover.html`

## Fail test run if coverage is below threshold

```go
package main

import (
	"fmt"
	"os"
	"testing"
)

func TestMain(m *testing.M) {
	// Run tests
	if m.Run() != 0 {
		fmt.Println("Test(s) failed")
		os.Exit(1)
	}

	// Check if coverage is enabled
	if testing.CoverMode() == "" {
		fmt.Println("Coverage is disabled")
		os.Exit(2)
	}

	// Check coverage
	minimumCoveragePercent := 80.0
	coveragePercent := testing.Coverage() * 100

	if coveragePercent < minimumCoveragePercent {
		fmt.Printf("Coverage is below %.1f%%!\n", minimumCoveragePercent)
		os.Exit(3)
	}

	// All good
	os.Exit(0)
}

```