Exploring existing namespaces

So you've got PyChromecast running and decided it is time to add support to your favorite app. No worries, the following instructions will have you covered in exploring the possibilities.

The following instructions require the use of the Google Chrome browser and the Google Cast plugin: 

1. In Chrome, go to chrome://net-internals/#capture

2. Enable the checkbox 'Include the actual bytes sent/received.'

3. Open a new tab, browse to your favorite application on the web that has Chromecast support and start casting.

4. Go back to the tab that is capturing events and click on stop.

5. From the dropdown click on events. This will show you a table with events that happened while you were recording.
6. In the filter box enter the text Tr@n$p0rt. This should give one SOCKET connection as result: the connection with your Chromecast.
7. Go through the results and collect the JSON that is exchanged.
8. Now write a controller that is able to mimic this behavior :-)