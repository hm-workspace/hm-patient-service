# hm-patient-service

Independent microservice repository for Hospital Management.

## Local run

`ash
dotnet restore
dotnet build
dotnet run --project src/PatientService.Api/PatientService.Api.csproj
`

## Docker

`ash
docker build -t hm-patient-service:local .
docker run -p 8082:8080 hm-patient-service:local
`

## GitHub setup later

`ash
git branch -M main
git remote add origin <your-github-repo-url>
git add .
git commit -m "Initial scaffold"
git push -u origin main
`
