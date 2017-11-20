import sys
import os
from shutil import copy
import urllib.request
import zipfile

arguments = sys.argv
if len(arguments) != 2:
    print("Missing problem name")
    print("Usage: init.py problem_name")
    quit()

problem_name = arguments[1]
print("Fetching problem: " + problem_name)

# make sure the problem exists
problem_url = "https://itkvdn.kattis.com/problems/" + problem_name
problem_request = urllib.request.Request(problem_url, headers={"User-Agent" : "Magic Browser"})
with urllib.request.urlopen(problem_request) as problem_request:
    if problem_request.getcode() != 200:
        print("Problem: " + problem_name + " does not exist!")
        quit()

# create a directory structure for the problem under this
problem_path = os.path.join("Problems", problem_name)
if os.path.exists(problem_path):
    print("Problem already initialized")
else:
    os.makedirs(problem_path)

    template_path = "Template"

    print("listing files in template path")
    for file_name in os.listdir(template_path):
        print("this file:" + file_name)
        template_file = os.path.join(template_path, file_name)
        if os.path.isfile(template_file):
            copy(template_file, problem_path)

    # download the test data
    data_zip = os.path.join(problem_path, "samples.zip")
    data_url = problem_url + "/file/statement/samples.zip"
    data_request = urllib.request.Request(data_url, headers={"User-Agent" : "Magic Browser"})
    with urllib.request.urlopen(data_request) as data_response:
        with open(data_zip, 'wb') as file:
            file.write(data_response.read())
            print("Test data downloaded")

    # unzip
    with zipfile.ZipFile(data_zip, 'r') as zip:
        zip.extractall(os.path.join(problem_path, "testdata"))

    # delete sample zip
    os.remove(data_zip)

    # create submit.bat
    submit_bat = os.path.join(problem_path, "submit.bat")
    with open(submit_bat, 'w') as submit_file:
        submit_file.write("python ..\..\submit.py Program.cs -p " + problem_name + " -f")

# launch IDE
os.system(os.path.join(problem_path, "Kattis.sln"))