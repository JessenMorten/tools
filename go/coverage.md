# Run tests and generate code coverage report

1. Run `go test -v -coverprofile=cover.text`
2. Run `go tool cover -html=cover.text -o cover.html`
3. Open `cover.html`
