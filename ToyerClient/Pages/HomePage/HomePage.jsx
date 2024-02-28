import classes from "./HomePage.module.css";
import { useEffect, useState } from "react";

export default function HomePage() {
  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  const [scrollY, setScrollY] = useState(window.scrollY);

  useEffect(() => {
    const handleScroll = () => {
      setScrollY(window.scrollY);
    };

    window.addEventListener("scroll", handleScroll);

    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, []);

  return (
    <div className={classes.homePageContainer}>
      <div className={classes.rowContainer}>
        <section className={classes.textSectionContainer}>
          <h3>Hello, I'm Adrian.</h3>
          <p>
            In March 2023 I started the process of transitioning towards web
            development. To achieve this, I completed a full-stack web developer
            training organized by the CodeCool group. The website you are
            currently viewing is the result of utilizing the skills acquired
            over the past year.
          </p>
        </section>
        <img
          className={classes.authorImg}
          src="https://i.postimg.cc/SQMH2FyQ/Adrian-Home-Small.jpg"
          alt="author"
        />
      </div>
      <div className={classes.rowContainer}>
        <img
          className={classes.motivesImg}
          src="https://i.postimg.cc/k5wKC37v/Motives-Collection.png"
          alt="motives"
        />
        <section className={classes.textSectionContainer}>
          <h3>My motivations.</h3>
          <p>
            The aim of this project was to create a solid base for controlling
            electronic devices over the worldwide network commonly known as the
            internet. Inspired by observing the problems related to the broad
            scope of human activity, I came to the conclusion that every company
            I visited or worked for was struggling with data flow issues. Remote
            control of electronic devices, often referred to in mass
            nomenclature as "smart," is becoming a standard. The continuous
            expansion of data transmission infrastructure worldwide clearly
            indicates development trends and opens up new possibilities. This
            project is my first attempt to delve into and manage this complex
            domain of human activity.
          </p>
        </section>
      </div>
      <div className={classes.rowContainer}>
        <section className={classes.textSectionContainer}>
          <h3>The results.</h3>
          <p>
            At this moment, I proudly present a web application that allows for
            safe user registration and assigning devices with a microcontroller
            with a Wi-Fi module to them. Anyone who owns a device programmed by
            me can assign it to their account and control it using this
            application. The network connection enables the utilization of cloud
            infrastructure provided by the Microsoft Azure IoT service.
          </p>
        </section>
        <img
          className={classes.reasonsImg}
          src="https://i.postimg.cc/KcDtnxmZ/Reasons.jpg"
          alt="reasons"
        />
      </div>
      <div className={classes.rowContainer}>
        <img
          className={classes.developmentImg}
          src="https://i.postimg.cc/Rh0fSpqk/development.jpg"
          alt="development"
        />
        <section className={classes.textSectionContainer}>
          <h3>Development.</h3>
          <p>
            Further planned steps for the development of the entire project will
            always prioritize focusing on programming skills first, and then on
            improving the user experience. The goal of creating the application
            is not material profit, but rather the desire to gain knowledge and
            experience. Every update introduced and planned will be provided for
            review in the patch notes section.
          </p>
        </section>
      </div>
      <div className={classes.rowContainer}>
        <section className={classes.textSectionContainer}>
          <h3>Tech stack.</h3>
          <p>
            Without further ado, here are the technologies I practiced and
            introduced into the project.
          </p>
        </section>
        <section className={classes.techStackContainer}>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/dVvkmmMk/htmlLogo.png"
              alt="HTML"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.CSSImg}
              src="https://i.postimg.cc/tgx5ZNF1/cssLogo.png"
              alt="CSS"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/dVDnvY1j/JSLogo.png"
              alt="JS"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/sg8mgNGd/react-Logo.png"
              alt="ReactJS"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.CSharpImg}
              src="https://i.postimg.cc/FsHv3DGn/C-Logo.png"
              alt="C#"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.aspImg}
              src="https://i.postimg.cc/DyvBjs3D/asp-Net-Logo.png"
              alt="Asp.net mvc"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/tTF2fNT0/azure-Logo.png"
              alt="Azure"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/br83jSDZ/azure-Sql-Logo.png"
              alt="Azure sql"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/ZYFwxH2p/IoTLogo.png"
              alt="Azure IoT"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/yYpHPcb6/CLogo.png"
              alt="C"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/FsTG2R9K/gitLogo.png"
              alt="Git"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/3RQZg0Zt/git-Hub-Logo.png"
              alt="Github"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/gjmx6mkS/docker-Logo.png"
              alt="Docker"
            />
          </span>
          <span className={classes.logoContainer}>
            <img
              className={classes.logoImg}
              src="https://i.postimg.cc/hPxSSp4J/platform-IOLogo.png"
              alt="PlatformIO"
            />
          </span>
        </section>
      </div>
      <button
        className={scrollY > 0 ? classes.scrollButton : classes.buttonHidden}
        onClick={scrollToTop}
      >
        ^
      </button>
    </div>
  );
}
