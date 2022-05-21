using DebuggingMultithreadedApp;

RunAllJobsOneByOne();
//RunAllJobsParallel();
//RunOnlyJobAIn4Threads();

void RunAllJobsOneByOne()
{
    Jobs jobs = new();

    jobs.JobA();

    jobs.JobB();

    jobs.JobC();
}

void RunAllJobsParallel()
{
    Jobs jobs = new();

    Thread threadA = new(jobs.JobA);
    threadA.Start();

    Thread threadB = new(jobs.JobB);
    threadB.Start();

    Thread threadC = new(jobs.JobC);
    threadC.Start();
}

void RunOnlyJobAIn4Threads()
{
    Jobs jobs = new();

    Thread threadA1 = new(jobs.JobA);
    threadA1.Start();

    Thread threadA2 = new(jobs.JobA);
    threadA2.Start();

    Thread threadA3 = new(jobs.JobA);
    threadA3.Start();

    Thread threadA4 = new(jobs.JobA);
    threadA4.Start();
}