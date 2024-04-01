pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = 'true'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DOTNET_CLI_HOME = tool name: 'dotnet-sdk-3.1.403', type: 'org.jenkinsci.plugins.tools.InstallSourceProperty$Installables', label: ''
    }

    stages {
        stage('Checkout') {
            steps {
                // Checkout your source code from version control
                git 'your_repository_url'
            }
        }

        stage('Restore') {
            steps {
                // Restore dependencies using the .NET Core CLI
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                // Build your ASP.NET Core application
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                echo 'this is Test ....'
                // Run tests using the .NET Core CLI
                sh 'dotnet test --no-restore --verbosity normal'
            }
        }

        stage('Publish') {
            steps {
                echo 'this is publish ....'
                // Publish your ASP.NET Core application
                sh 'dotnet publish --no-restore --no-build --configuration Release --output ./publish'
            }
        }

      
        stage('xxxxxxxxxxxx') {
            steps {
                echo 'this is xxxxxxxxxxxx ....'
                // Publish your ASP.NET Core application
                sh 'dotnet publish --no-restore --no-build --configuration Release --output ./publish'
            }
        }

      
        stage('Deploy') {
            steps {
                echo 'Deploy ....'

                // Perform deployment steps here, such as copying the published files to your server
                // You may need additional plugins or scripts for this depending on your deployment strategy
            }
        }
    }

}
